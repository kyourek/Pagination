using System.Linq;

using NUnit.Framework;

namespace Pagination.Test {
    [TestFixture, TestOf(typeof(PageLinker))]
    public class PageLinkerTest {
        Page Page => _Page ?? (_Page = new Page());
        Page _Page;

        IPageLinker Subject => _Subject ?? (_Subject = new PageLinker(Page));
        IPageLinker _Subject;

        [SetUp]
        public void SetUp() {
            _Page = null;
            _Subject = null;
        }

        #region Prev
        [Test]
        public void Prev_GetsSingleLink() {
            Assert.That(
                Subject.Prev().Links.Count(),
                Is.EqualTo(1));
        }

        [Test]
        public void Prev_GetsNoLinksIfNotForced() {
            Assert.That(
                Subject.Prev(force: false).Links.Count(),
                Is.Zero);
        }

        [Test]
        public void Prev_GetsSingleLinkIfAvailable() {
            Page.PageBaseZero = 1;
            Assert.That(
                Subject.Prev(force: false).Links.Count(),
                Is.EqualTo(1));
        }

        [Test]
        public void Prev_DefaultTextIsLessThan() {
            Assert.That(
                Subject.Prev().Links.First().LinkText,
                Is.EqualTo("<"));
        }

        [Test]
        public void Prev_LinkTextIsTakenFromArgument() {
            Assert.That(
                Subject.Prev(text: "PREV").Links.First().LinkText,
                Is.EqualTo("PREV"));
        }

        [Test]
        public void Prev_LinkPageIsZero() {
            Assert.That(
                Subject.Prev().Links.First().LinkPageBaseZero,
                Is.Zero);
        }

        [Test]
        public void Prev_LinkPageBaseOneIsOne() {
            Assert.That(
                Subject.Prev().Links.First().LinkPageBaseOne,
                Is.EqualTo(1));
        }

        [Test]
        public void Prev_LowerPageBaseZeroEqualsLinkPageBaseZero() {
            Page.PageBaseZero = 5;
            Assert.That(
                Subject.Prev().Links.First().LowerPageBaseZero,
                Is.EqualTo(4));
        }

        [Test]
        public void Prev_UpperPageBaseZeroEqualsLinkPageBaseZero() {
            Page.PageBaseZero = 11;
            Assert.That(
                Subject.Prev().Links.First().UpperPageBaseZero,
                Is.EqualTo(10));
        }

        [Test]
        public void Prev_LowerPageBaseOneEqualsLinkPageBaseOne() {
            Page.PageBaseZero = 5;
            Assert.That(
                Subject.Prev().Links.First().LowerPageBaseOne,
                Is.EqualTo(5));
        }

        [Test]
        public void Prev_UpperPageBaseOneEqualsLinkPageBaseOne() {
            Page.PageBaseZero = 11;
            Assert.That(
                Subject.Prev().Links.First().UpperPageBaseOne,
                Is.EqualTo(11));
        }

        [Test]
        public void Prev_IsPageRangeIsFalse() {
            Assert.That(
                Subject.Prev().Links.First().IsPageRange,
                Is.False);
        }

        [Test]
        public void Prev_IsRequestedPageIsTrueIfNonePreceding() {
            Assert.That(
                Subject.Prev().Links.First().IsRequestedPage,
                Is.True);
        }

        [Test]
        public void Prev_IsRequestedPageIsFalseIfNotFirstPage() {
            Page.PageBaseZero = 1;
            Assert.That(
                Subject.Prev().Links.First().IsRequestedPage,
                Is.False);
        }

        [Test]
        public void Prev_PageIsInstance() {
            Assert.That(
                Subject.Prev().Links.First().Page,
                Is.SameAs(Page));
        }
        #endregion

        #region Next
        [Test]
        public void Next_GetsSingleLink() {
            Assert.That(
                Subject.Next().Links.Count(),
                Is.EqualTo(1));
        }

        [Test]
        public void Next_GetsNoLinksIfNotForced() {
            Assert.That(
                Subject.Next(force: false).Links.Count(),
                Is.Zero);
        }

        [Test]
        public void Next_GetsSingleLinkIfAvailable() {
            Page.PageBaseZero = 1;
            Page.PagesTotal = 3;
            Assert.That(
                Subject.Next(force: false).Links.Count(),
                Is.EqualTo(1));
        }

        [Test]
        public void Next_DefaultTextIsGreaterThan() {
            Assert.That(
                Subject.Next().Links.First().LinkText,
                Is.EqualTo(">"));
        }

        [Test]
        public void Next_LinkTextIsTakenFromArgument() {
            Assert.That(
                Subject.Next(text: "Next").Links.First().LinkText,
                Is.EqualTo("Next"));
        }

        [Test]
        public void Next_LinkPageIsZero() {
            Assert.That(
                Subject.Next().Links.First().LinkPageBaseZero,
                Is.Zero);
        }

        [Test]
        public void Next_LinkPageBaseOneIsOne() {
            Assert.That(
                Subject.Next().Links.First().LinkPageBaseOne,
                Is.EqualTo(1));
        }

        [Test]
        public void Next_LowerPageBaseZeroEqualsLinkPageBaseZero() {
            Page.PageBaseZero = 5;
            Assert.That(
                Subject.Next().Links.First().LowerPageBaseZero,
                Is.EqualTo(5));
        }

        [Test]
        public void Next_UpperPageBaseZeroEqualsLinkPageBaseZero() {
            Page.PageBaseZero = 11;
            Assert.That(
                Subject.Next().Links.First().UpperPageBaseZero,
                Is.EqualTo(11));
        }

        [Test]
        public void Next_LowerPageBaseOneEqualsLinkPageBaseOne() {
            Page.PageBaseZero = 5;
            Assert.That(
                Subject.Next().Links.First().LowerPageBaseOne,
                Is.EqualTo(6));
        }

        [Test]
        public void Next_UpperPageBaseOneEqualsLinkPageBaseOne() {
            Page.PageBaseZero = 11;
            Assert.That(
                Subject.Next().Links.First().UpperPageBaseOne,
                Is.EqualTo(12));
        }

        [Test]
        public void Next_IsPageRangeIsFalse() {
            Assert.That(
                Subject.Next().Links.First().IsPageRange,
                Is.False);
        }

        [Test]
        public void Next_IsRequestedPageIsTrueIfNoneFollowing() {
            Assert.That(
                Subject.Next().Links.First().IsRequestedPage,
                Is.True);
        }

        [Test]
        public void Next_IsRequestedPageIsFalseIfNotLastPage() {
            Page.PagesTotal = 2;
            Assert.That(
                Subject.Next().Links.First().IsRequestedPage,
                Is.False);
        }

        [Test]
        public void Next_PageIsInstance() {
            Assert.That(
                Subject.Next().Links.First().Page,
                Is.SameAs(Page));
        }
        #endregion

        #region Numbers
        [Test]
        public void Numbers_HasOneLinkPerPage() {
            Page.PagesTotal = 17;
            Assert.That(
                Subject.Numbers().Links.Count(),
                Is.EqualTo(17));
        }

        [Test]
        public void Numbers_HasLinkForEachPage() {
            Page.PagesTotal = 8;
            var actual = Subject.Numbers().Links.Select(link => link.LinkPageBaseZero);
            var expected = Enumerable.Range(0, Page.PagesTotal);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Numbers_LinkTextIsPageBaseOne() {
            Page.PagesTotal = 11;
            Assert.That(
                Subject.Numbers().Links.Select(link => link.LinkText),
                Is.EqualTo(Enumerable.Range(0, Page.PagesTotal).Select(page => (page + 1).ToString())));
        }

        [Test]
        public void Numbers_ContainsOneRequestedPage() {
            Page.PageBaseZero = 5;
            Page.PagesTotal = 21;
            Assert.That(
                Subject.Numbers().Links.Count(link => link.IsRequestedPage),
                Is.EqualTo(1));
        }

        [Test]
        public void Numbers_RequestedPageIsPageBaseZeroOfPage() {
            Page.PageBaseZero = 5;
            Page.PagesTotal = 21;
            Assert.That(
                Subject.Numbers().Links.First(link => link.IsRequestedPage).LinkPageBaseZero,
                Is.EqualTo(Page.PageBaseZero));
        }

        [Test]
        public void Numbers_NoneArePageRange() {
            Page.PagesTotal = 56;
            Assert.That(
                Subject.Numbers().Links,
                Has.None.Property("IsPageRange").True);
        }

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
        #endregion

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
