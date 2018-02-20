using System.Linq;

using NUnit.Framework;

namespace Pagination.Linking.Tests {
    [TestFixture]
    public class PrevNextLinkerTests {
        [Test]
        public void LinkPages_ReturnsOnlyPrevAndNextWithNullBaseLinker() {
            var linker = new PrevNextLinker {
                ForcePrevNext = true,
                NextText = "next",
                PrevText = "prev"
            };

            var req = new PageRequest { ItemsPerPage = 10, PageBaseZero = 2 };
            var src = new PageSource<int> { Request = req, Query = Enumerable.Range(1, 50).AsQueryable().OrderBy(i => i) };
            var page = src.FindPage();
            var linkedPages = linker.Links(page);
            Assert.IsNotNull(linkedPages);
            Assert.AreEqual(2, linkedPages.Count());

            var link1 = linkedPages.ElementAt(0);
            var link2 = linkedPages.ElementAt(1);

            Assert.AreEqual("prev", link1.LinkText);
            Assert.AreEqual("next", link2.LinkText);
            Assert.AreEqual(1, link1.LinkPageBaseZero);
            Assert.AreEqual(3, link2.LinkPageBaseZero);
            Assert.AreSame(page, link1.Page);
            Assert.AreSame(page, link2.Page);
            Assert.IsFalse(link1.IsPageRange);
            Assert.IsFalse(link2.IsPageRange);
            Assert.IsFalse(link1.IsRequestedPage);
            Assert.IsFalse(link2.IsRequestedPage);
        }
    }
}
