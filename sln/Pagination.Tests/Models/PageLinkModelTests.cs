using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Pagination.Models.Tests {

    [TestFixture]
    public class PageLinkModelTests {
        
        [Test]
        public void Constructor_SetsProperties() {
            var request = new PageRequestModel { RequestedPage = 11 };
            var linkPage = 11;
            var linkText = "eleven";
            var o = new PageLinkModel(request, linkPage, linkText);
            Assert.AreSame(request, o.Request);
            Assert.AreEqual(linkPage, o.LinkPage);
            Assert.AreEqual(linkText, o.LinkText);
            Assert.AreEqual(o.LinkPage, o.LowerPage);
            Assert.AreEqual(o.LinkPage, o.UpperPage);
            Assert.IsFalse(o.IsPageRange);
            Assert.IsTrue(o.IsRequestedPage);
        }

        [Test]
        public void Constructor_SetsIsRequestedPageFalseWhenNotRequestedPage() {
            var request = new PageRequestModel { RequestedPage = 22 };
            var o = new PageLinkModel(request, 21, "21");
            Assert.IsFalse(o.IsRequestedPage);
        }

        [Test]
        public void Constructor_ThrowsExceptionWhenRequestIsNull() {
            try {
                new PageLinkModel(null, 21, "21");
            }
            catch (Exception ex) {
                Assert.IsInstanceOf(typeof(ArgumentNullException), ex);
                return;
            }
            Assert.Fail("No exception thrown.");
        }
    }
}
