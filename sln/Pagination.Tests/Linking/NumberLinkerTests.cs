//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;

//using Moq;
//using NUnit.Framework;
//using Pagination.Models;

//namespace Pagination.Linking.Tests {

//    [TestFixture]
//    public class NumberLinkerTests {
    
//        [Test]
//        public void Constructor_SetsProperties() {
//            var l = new NumberLinker(false);
//            Assert.IsFalse(l.IsBase1);
//            l = new NumberLinker(true);
//            Assert.IsTrue(l.IsBase1);
//        }

//        [Test]
//        public void LinkPagse_LinksBase1PagesInSuccession() {
//            var request = new Mock<IPageRequestModel>();
//            var results = new Mock<IPageResultsModel>();

//            request.Setup(r => r.RequestedPage).Returns(8);
//            results.Setup(r => r.TotalPageCount).Returns(17);

//            var linker = new NumberLinker(true);
//            var linked = linker.LinkPages(request.Object, results.Object);
//            Assert.AreEqual(17, linked.Count());
//            for (var i = 0; i < linked.Count(); i++) {
//                var l = linked.ElementAt(i);
//                Assert.AreEqual(i, l.LinkPage);
//                Assert.AreEqual((i + 1).ToString(), l.LinkText);
//                Assert.AreEqual(8 == i, l.IsRequestedPage);
//                Assert.IsFalse(l.IsPageRange);
//            }
//        }

//        [Test]
//        public void LinkPages_LinksBase0PagesInSuccession() {
//            var request = new Mock<IPageRequestModel>();
//            var results = new Mock<IPageResultsModel>();

//            request.Setup(r => r.RequestedPage).Returns(41);
//            results.Setup(r => r.TotalPageCount).Returns(176);

//            var linker = new NumberLinker(false);
//            var linked = linker.LinkPages(request.Object, results.Object);
//            Assert.AreEqual(176, linked.Count());
//            for (var i = 0; i < linked.Count(); i++) {
//                var l = linked.ElementAt(i);
//                Assert.AreEqual(i, l.LinkPage);
//                Assert.AreEqual(i.ToString(), l.LinkText);
//                Assert.AreEqual(41 == i, l.IsRequestedPage);
//                Assert.IsFalse(l.IsPageRange);
//            }
//        }
//    }
//}
