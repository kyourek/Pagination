using System.Linq;

using NUnit.Framework;

namespace Pagination.Linking.Tests {
    [TestFixture]
    public class NumberLinkerTests {
        [Test]
        public void LinkPagse_LinksBase1PagesInSuccession() {
            var page = new Page {
                PageBaseZero = 8,
                PageTotal = 17
            };

            var linker = new NumberLinker { BaseOne = true };
            var linked = linker.LinkPages(page);
            Assert.AreEqual(17, linked.Count());
            for (var i = 0; i < linked.Count(); i++) {
                var l = linked.ElementAt(i);
                Assert.AreEqual(i, l.LinkPageBaseZero);
                Assert.AreEqual((i + 1).ToString(), l.LinkText);
                Assert.AreEqual(8 == i, l.IsRequestedPage);
                Assert.IsFalse(l.IsPageRange);
            }
        }

        [Test]
        public void LinkPages_LinksBase0PagesInSuccession() {
            var page = new Page {
                PageBaseZero = 41,
                PageTotal = 176
            };

            var linker = new NumberLinker();
            var linked = linker.LinkPages(page);
            Assert.AreEqual(176, linked.Count());
            for (var i = 0; i < linked.Count(); i++) {
                var l = linked.ElementAt(i);
                Assert.AreEqual(i, l.LinkPageBaseZero);
                Assert.AreEqual(i.ToString(), l.LinkText);
                Assert.AreEqual(41 == i, l.IsRequestedPage);
                Assert.IsFalse(l.IsPageRange);
            }
        }
    }
}
