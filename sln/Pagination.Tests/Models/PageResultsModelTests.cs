using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using Pagination.Abstractions;

namespace Pagination.Models.Tests {

    [TestFixture]
    public class PageResultsModelTests {

        [Test]
        public void Constructor_CreatesNewObjectWithSpecifiedArguments() {
            var source = new Mock<IOrderedPageSourceModel<object, PageRequestModel>>();
            var results = new Mock<ISource<object>>();
            var m = new PageResultsModel<object, PageRequestModel>(97, 195, source.Object, results.Object);
            Assert.AreEqual(97, m.TotalPageCount);
            Assert.AreEqual(195, m.TotalItemCount);
            Assert.AreSame(source.Object, m.Source);
            Assert.AreSame(results.Object, m.Results);
        }

        [Test]
        public void LinkPages_UsesPageLinker() {
            var source = new Mock<IOrderedPageSourceModel<object, PageRequestModel>>();
            var results = new Mock<ISource<object>>();
            var m = new PageResultsModel<object, PageRequestModel>(97, 195, source.Object, results.Object);

            var pageLinker = new Mock<PageLinker>();
            var linkedPageCollection = new List<IPageLinkModel>();
            pageLinker.Setup(l => l.LinkPages(m.Source.Request, m)).Returns(linkedPageCollection).Verifiable();

            var linkedPages = m.LinkPages(pageLinker.Object);
            pageLinker.VerifyAll();

            Assert.AreEqual(m.TotalItemCount, linkedPages.TotalItemCount);
            Assert.AreEqual(m.TotalPageCount, linkedPages.TotalPageCount);
            Assert.AreEqual(linkedPageCollection.GetEnumerator(), linkedPages.GetEnumerator());
        }

        [Test]
        public void LinkPages_UsesDefaultPageLinker() {
            var source = new Mock<IOrderedPageSourceModel<object, PageRequestModel>>();
            var results = new Mock<ISource<object>>();
            var m = new PageResultsModel<object, PageRequestModel>(97, 195, source.Object, results.Object);

            var pageLinker = new Mock<PageLinker>();
            var linkedPageCollection = new List<IPageLinkModel>();
            pageLinker.Setup(l => l.LinkPages(m.Source.Request, m)).Returns(linkedPageCollection).Verifiable();

            PageLinker.Default = pageLinker.Object;
            var linkedPages = m.LinkPages();
            pageLinker.VerifyAll();

            Assert.AreEqual(m.TotalItemCount, linkedPages.TotalItemCount);
            Assert.AreEqual(m.TotalPageCount, linkedPages.TotalPageCount);
            Assert.AreEqual(linkedPageCollection.GetEnumerator(), linkedPages.GetEnumerator());
        }
    }
}
