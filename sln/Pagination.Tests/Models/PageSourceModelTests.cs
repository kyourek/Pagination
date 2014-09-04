using System;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using Pagination.Abstractions;

namespace Pagination.Models.Tests {

    [TestFixture]
    public class PageSourceModelTests {

        [Test]
        public void Constructor_CreatesInstanceWithSpecifiedArguments() {
            var maxItemsPerPage = 18;
            var defaultItemsPerPage = 7;
            var source = new Source<object>(new List<object>().AsQueryable());
            var request = new PageRequestModel();
            var m = new PageSourceModel<object, PageRequestModel>(maxItemsPerPage, defaultItemsPerPage, source, request);
            Assert.AreEqual(maxItemsPerPage, m.GetMaxItemsPerPage());
            Assert.AreEqual(defaultItemsPerPage, m.GetDefaultItemsPerPage());
            Assert.AreSame(source, m.Source);
            Assert.AreSame(request, m.Request);
        }

        [Test]
        public void MaxItemsPerPage_ReturnsNewInstanceWithMaxItemsPerPage() {
            var m1 = new PageSourceModel<object, PageRequestModel>(26, 1, new Source<object>(new List<object>().AsQueryable()), new PageRequestModel());
            Assert.AreEqual(26, m1.GetMaxItemsPerPage());
            var m2 = m1.MaxItemsPerPage(14);
            Assert.AreNotSame(m1, m2);
            Assert.AreNotEqual(m1, m2);
            Assert.AreEqual(14, m2.GetMaxItemsPerPage());
        }

        [Test]
        public void DefaultItemsPerPage_ReturnsNewInstanceWithDefaultItemsPerPage() {
            var m1 = new PageSourceModel<object, PageRequestModel>(1, 24, new Source<object>(new List<object>().AsQueryable()), new PageRequestModel());
            Assert.AreEqual(24, m1.GetDefaultItemsPerPage());
            var m2 = m1.DefaultItemsPerPage(16);
            Assert.AreNotSame(m1, m2);
            Assert.AreNotEqual(m1, m2);
            Assert.AreEqual(16, m2.GetDefaultItemsPerPage());
        }

        [Test]
        public void OrderBy_ReturnsOrderedSource() {
            var source = new Mock<IOrderedSource<string>>(MockBehavior.Strict);
            Expression<Func<string, int>> keySelector = s => s.Length;
            var orderedSource = new OrderedSource<string>(new List<string>().AsQueryable().OrderBy(keySelector));
            var m1 = new PageSourceModel<string, PageRequestModel>(3, 4, source.Object, new PageRequestModel());

            source.Setup(s => s.OrderBy(keySelector)).Returns(orderedSource).Verifiable("The source was not ordered by the key selector.");

            var m2 = m1.OrderBy(keySelector);
            source.Verify();
            Assert.AreEqual(m1.GetMaxItemsPerPage(), m2.GetMaxItemsPerPage());
            Assert.AreEqual(m1.GetDefaultItemsPerPage(), m2.GetDefaultItemsPerPage());
            Assert.AreSame(m1.Request, m2.Request);
            Assert.AreSame(orderedSource, m2.Source);
        }

        [Test]
        public void OrderByDescending_ReturnsOrderedByDescendingSource() {
            var source = new Mock<IOrderedSource<string>>(MockBehavior.Strict);
            Expression<Func<string, int>> keySelector = s => s.Length;
            var orderedSource = new OrderedSource<string>(new List<string>().AsQueryable().OrderByDescending(keySelector));
            var m1 = new PageSourceModel<string, PageRequestModel>(3, 4, source.Object, new PageRequestModel());

            source.Setup(s => s.OrderByDescending(keySelector)).Returns(orderedSource).Verifiable("The source was not ordered by descending by the key selector.");

            var m2 = m1.OrderByDescending(keySelector);
            source.Verify();
            Assert.AreEqual(m1.GetMaxItemsPerPage(), m2.GetMaxItemsPerPage());
            Assert.AreEqual(m1.GetDefaultItemsPerPage(), m2.GetDefaultItemsPerPage());
            Assert.AreSame(m1.Request, m2.Request);
            Assert.AreSame(orderedSource, m2.Source);
        }
    }
}
