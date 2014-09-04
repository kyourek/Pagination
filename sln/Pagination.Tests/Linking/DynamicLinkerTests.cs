using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using Pagination.Models;

namespace Pagination.Linking.Tests {

    [TestFixture]
    public class DynamicLinkerTests {

        [Test]
        public void Constructor_SetsProperties() {
            var linker = new DynamicLinker(true);
            Assert.IsTrue(linker.IsBase1);
            linker = new DynamicLinker(false);
            Assert.IsFalse(linker.IsBase1);
        }

        [Test]
        public void LinkPages_ReturnsLinkedPages() {
            var linker = new DynamicLinker(true);

            var request = new Mock<IPageRequestModel>();
            var results = new Mock<IPageResultsModel>();

            request.Setup(r => r.RequestedPage).Returns(1);
            results.Setup(r => r.TotalPageCount).Returns(56);

            var linked = linker.LinkPages(request.Object, results.Object);
            Assert.IsNotNull(linked);

            var p0 = linked.ElementAt(0);
            Assert.AreEqual(0, p0.LinkPage);
            Assert.AreEqual("1", p0.LinkText);
            Assert.AreEqual(p0.LinkPage, p0.LowerPage);
            Assert.AreEqual(p0.LinkPage, p0.UpperPage);
            Assert.IsFalse(p0.IsPageRange);
            Assert.AreSame(request.Object, p0.Request);

            var p1 = linked.ElementAt(1);
            Assert.AreEqual(1, p1.LinkPage);
            Assert.AreEqual("2", p1.LinkText);
            Assert.AreEqual(p1.LinkPage, p1.LowerPage);
            Assert.AreEqual(p1.LinkPage, p1.UpperPage);
            Assert.IsFalse(p1.IsPageRange);
            Assert.AreSame(request.Object, p1.Request);

            var p2 = linked.ElementAt(2);
            Assert.AreEqual(2, p2.LinkPage);
            Assert.AreEqual("3", p2.LinkText);
            Assert.AreEqual(p2.LinkPage, p2.LowerPage);
            Assert.AreEqual(p2.LinkPage, p2.UpperPage);
            Assert.IsFalse(p2.IsPageRange);
            Assert.AreSame(request.Object, p2.Request);

            var p3 = linked.ElementAt(3);
            Assert.AreEqual(3, p3.LinkPage);
            Assert.AreEqual("...", p3.LinkText);
            Assert.AreEqual(3, p3.LowerPage);
            Assert.AreEqual(54, p3.UpperPage);
            Assert.IsTrue(p3.IsPageRange);
            Assert.AreSame(request.Object, p3.Request);

            var p4 = linked.ElementAt(4);
            Assert.AreEqual(55, p4.LinkPage);
            Assert.AreEqual("56", p4.LinkText);
            Assert.AreEqual(p4.LinkPage, p4.LowerPage);
            Assert.AreEqual(p4.LinkPage, p4.UpperPage);
            Assert.IsFalse(p4.IsPageRange);
            Assert.AreSame(request.Object, p4.Request);

            Assert.IsNull(linked.ElementAtOrDefault(5));
        }
    }
}
