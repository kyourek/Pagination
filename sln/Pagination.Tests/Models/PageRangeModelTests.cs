using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Pagination.Models.Tests {

    [TestFixture]
    public class PageRangeModelTests {
    
        [Test]
        public void Constructor_SetsProperties() {
            var request = new PageRequestModel { RequestedPage = 8 };
            var lowerPage = 8;
            var upperPage = 10;
            var o = new PageRangeModel(request, lowerPage, upperPage);
            Assert.AreSame(request, o.Request);
            Assert.AreEqual(lowerPage, o.LowerPage);
            Assert.AreEqual(upperPage, o.UpperPage);
            Assert.AreEqual(o.LowerPage, o.LinkPage);
            Assert.AreEqual("...", o.LinkText);
            Assert.IsFalse(o.IsRequestedPage);
            Assert.IsTrue(o.IsPageRange);
        }
    }
}
