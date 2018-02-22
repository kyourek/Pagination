using System.Linq;

using NUnit.Framework;

namespace Pagination.Test {
    [TestFixture, TestOf(typeof(PageLinker))]
    public class PageLinkerTest {
        [Test]
        public void Numbers_LinksBase1PagesInSuccession() {
            var page = new Page { PageBaseZero = 8, PagesTotal = 17 };
            var subject = new PageLinker(page);
            var links = subject.Numbers(baseOne: true).Links;
            Assert.AreEqual(17, links.Count());
            for (var i = 0; i < links.Count(); i++) {
                var l = links.ElementAt(i);
                Assert.AreEqual(i, l.LinkPageBaseZero);
                Assert.AreEqual((i + 1).ToString(), l.LinkText);
                Assert.AreEqual(8 == i, l.IsRequestedPage);
                Assert.IsFalse(l.IsPageRange);
            }
        }

        [Test]
        public void Numbers_LinksBase0PagesInSuccession() {
            var page = new Page { PageBaseZero = 41, PagesTotal = 176 };
            var subject = new PageLinker(page);
            var links = subject.Numbers(baseOne: false).Links;
            Assert.AreEqual(176, links.Count());
            for (var i = 0; i < links.Count(); i++) {
                var l = links.ElementAt(i);
                Assert.AreEqual(i, l.LinkPageBaseZero);
                Assert.AreEqual(i.ToString(), l.LinkText);
                Assert.AreEqual(41 == i, l.IsRequestedPage);
                Assert.IsFalse(l.IsPageRange);
            }
        }

        [Test]
        public void Prev_ReturnsPrevThenNext() {
            var req = new PageRequest { ItemsPerPage = 10, PageBaseZero = 2 };
            var src = new PageSource<int> { Request = req, ItemsSource = Enumerable.Range(1, 50).AsQueryable().OrderBy(i => i) };
            var page = src.FindPage();
            var subject = new PageLinker(page);
            var links = subject.Prev("prev", true).Next("next", true).Links;
            Assert.IsNotNull(links);
            Assert.AreEqual(2, links.Count());

            var link1 = links.ElementAt(0);
            var link2 = links.ElementAt(1);

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

        [Test]
        public void Dynamic_HasCorrectLinks() {
            var page = new Page { PageBaseZero = 1, PagesTotal = 56 };
            var subject = new PageLinker(page);

            var links = subject.Dynamic(baseOne: true).Links;
            Assert.IsNotNull(links);

            var p0 = links.ElementAt(0);
            Assert.AreEqual(0, p0.LinkPageBaseZero);
            Assert.AreEqual("1", p0.LinkText);
            Assert.AreEqual(p0.LinkPageBaseZero, p0.LowerPageBaseZero);
            Assert.AreEqual(p0.LinkPageBaseZero, p0.UpperPageBaseZero);
            Assert.IsFalse(p0.IsPageRange);
            Assert.AreSame(page, p0.Page);

            var p1 = links.ElementAt(1);
            Assert.AreEqual(1, p1.LinkPageBaseZero);
            Assert.AreEqual("2", p1.LinkText);
            Assert.AreEqual(p1.LinkPageBaseZero, p1.LowerPageBaseZero);
            Assert.AreEqual(p1.LinkPageBaseZero, p1.UpperPageBaseZero);
            Assert.IsFalse(p1.IsPageRange);
            Assert.AreSame(page, p1.Page);

            var p2 = links.ElementAt(2);
            Assert.AreEqual(2, p2.LinkPageBaseZero);
            Assert.AreEqual("3", p2.LinkText);
            Assert.AreEqual(p2.LinkPageBaseZero, p2.LowerPageBaseZero);
            Assert.AreEqual(p2.LinkPageBaseZero, p2.UpperPageBaseZero);
            Assert.IsFalse(p2.IsPageRange);
            Assert.AreSame(page, p2.Page);

            var p3 = links.ElementAt(3);
            Assert.AreEqual(3, p3.LinkPageBaseZero);
            Assert.AreEqual("...", p3.LinkText);
            Assert.AreEqual(3, p3.LowerPageBaseZero);
            Assert.AreEqual(54, p3.UpperPageBaseZero);
            Assert.IsTrue(p3.IsPageRange);
            Assert.AreSame(page, p3.Page);

            var p4 = links.ElementAt(4);
            Assert.AreEqual(55, p4.LinkPageBaseZero);
            Assert.AreEqual("56", p4.LinkText);
            Assert.AreEqual(p4.LinkPageBaseZero, p4.LowerPageBaseZero);
            Assert.AreEqual(p4.LinkPageBaseZero, p4.UpperPageBaseZero);
            Assert.IsFalse(p4.IsPageRange);
            Assert.AreSame(page, p4.Page);

            Assert.IsNull(links.ElementAtOrDefault(5));
        }
    }
}
