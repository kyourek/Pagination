using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Pagination.Models;

namespace Pagination.Linking.Tests {

    [TestFixture]
    public class PrevNextLinkerTests {

        [Test]
        public void Constructor_SetsProperties() {
            var baseLinker = new NumberLinker(true);
            var linker = new PrevNextLinker(baseLinker, "v", "x", false);
            Assert.AreSame(baseLinker, linker.BaseLinker);
            Assert.AreEqual("v", linker.PrevText);
            Assert.AreEqual("x", linker.NextText);
            Assert.IsFalse(linker.ForcePrevNext);
        }

        [Test]
        public void LinkPages_ReturnsOnlyPrevAndNextWithNullBaseLinker() {
            var linker = new PrevNextLinker(null, "prev", "next", true);
            var source = new PageSourceFactory().CreateSource(Enumerable.Range(1, 50).AsQueryable(), 2, 10);
            var request = source.Request;
            var results = source.OrderBy(i => i).Query();
            var linkedPages = linker.LinkPages(request, results);
            Assert.IsNotNull(linkedPages);
            Assert.AreEqual(2, linkedPages.Count());

            var link1 = linkedPages.ElementAt(0);
            var link2 = linkedPages.ElementAt(1);

            Assert.AreEqual("prev", link1.LinkText);
            Assert.AreEqual("next", link2.LinkText);
            Assert.AreEqual(1, link1.LinkPage);
            Assert.AreEqual(3, link2.LinkPage);
            Assert.AreSame(request, link1.Request);
            Assert.AreSame(request, link2.Request);
            Assert.IsFalse(link1.IsPageRange);
            Assert.IsFalse(link2.IsPageRange);
            Assert.IsFalse(link1.IsRequestedPage);
            Assert.IsFalse(link2.IsRequestedPage);
        }
    }
}
