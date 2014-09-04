using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Pagination.Models.Tests {
    
    [TestFixture]
    public class PageChainModelTests {
    
        [Test]
        public void Constructor_SetsProperties() {
            var r = new PageRequestModel();
            var e = new PageLinkModelBase[] { new PageLinkModel(r, 1, "1"), new PageRangeModel(r, 2, 4) };
            var m = new PageChainModel(23, 312, e);
            Assert.AreEqual(23, m.TotalPageCount);
            Assert.AreEqual(312, m.TotalItemCount);
            Assert.AreSame(e, m.PageLinks);
        }

        [Test]
        public void Constructor_ThrowsExceptionIfPageLinksIsNull() {
            try {
                new PageChainModel(1, 2, null);
            }
            catch (Exception e) {
                Assert.IsInstanceOf(typeof(ArgumentNullException), e);
                return;
            }
            Assert.Fail("No exception thrown.");
        }

        [Test]
        public void Enumerator_ComesFromPageLinks() {
            var r = new PageRequestModel();
            var e = new PageLinkModelBase[] { new PageLinkModel(r, 1, "1"), new PageRangeModel(r, 2, 4) };
            var m = new PageChainModel(432, 321, e);
            Assert.AreEqual(e.Count(), m.Count());
            for (var i = 0; i < e.Count(); i++) {
                Assert.AreSame(e.ElementAt(i), m.ElementAt(i));
            }
        }
    }
}
