using System;
using System.Linq;

using NUnit.Framework;

namespace Pagination.Test {
    [TestFixture, TestOf(typeof(PageContext))]
    public class PageContextTest {
        PageContext _Subject;
        PageContext Subject => _Subject ?? (_Subject = new PageContext());

        [TearDown]
        public void TearDown() {
            _Subject = null;
        }

        #region FindPage
        [Test]
        public void FindPage_FindsFirstPageOfItems() {
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.Items;
            var expected = source.Take(Subject.Config.ItemsPerPageDefault);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void FindPage_FindsNthPageOfItems(int n) {
            Subject.GetRequest = () => new PageRequest { PageBaseZero = n };
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.Items;
            var expected = source
                .Skip(n * Subject.Config.ItemsPerPageDefault)
                .Take(Subject.Config.ItemsPerPageDefault);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void FindPage_ConsidersItemsPerPageMaximum() {
            Subject.GetRequest = () => new PageRequest { ItemsPerPage = 54321 };
            Subject.SetItemsPerPageMaximum(99);
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.ItemsPerPage;
            var expected = 99;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindPage_SetsStateObject() {
            var state = new object();
            var page = Subject.FindPage(new object[] { }.AsQueryable().OrderBy(obj => obj), state);
            Assert.That(page.State, Is.SameAs(state));
        }

        [Test]
        public void FindPage_ReturnsNoItemsIfItemsSourceIsNull() {
            var page = Subject.FindPage(default(IOrderedQueryable<string>));
            Assert.That(page.Items.Count(), Is.Zero);
        }

        [Test]
        public void FindPage_SetsItemsTotalOfPage() {
            var source = Enumerable
                .Range(0, 8761)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            Assert.That(page.ItemsTotal, Is.EqualTo(8761));
        }

        [TestCase(7250, 25)]
        public void FindPage_SetsPagesTotalOfPage(int itemsTotal, int itemsPerPage) {
            Subject.GetRequest = () => new PageRequest { ItemsPerPage = itemsPerPage };
            var source = Enumerable
                .Range(0, itemsTotal)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.PagesTotal;
            var expected = (int)Math.Ceiling((double)itemsTotal / itemsPerPage);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindPage_SetsConfigOfPage() {
            var page = Subject.FindPage(default(IOrderedQueryable<string>));
            Assert.That(page.Config, Is.SameAs(Subject.Config));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void FindPage_SetsPageBaseZeroOfPage(int value) {
            Subject.GetRequest = () => new PageRequest { PageBaseZero = value };
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            Assert.That(page.PageBaseZero, Is.EqualTo(value));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void FindPage_SetsPageBaseOneOfPage(int value) {
            Subject.GetRequest = () => new PageRequest { PageBaseZero = value - 1 };
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            Assert.That(page.PageBaseOne, Is.EqualTo(value));
        }
        #endregion

        #region SetItemsPerPageDefault
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(20)]
        [TestCase(100)]
        public void SetItemsPerPageDefault_SetsConfigValue(int value) {
            Subject.SetItemsPerPageDefault(value);
            Assert.That(Subject.Config.ItemsPerPageDefault, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPageDefault_ReturnsThisInstance() {
            Assert.That(Subject.SetItemsPerPageDefault(1), Is.SameAs(Subject));
        }
        #endregion

        #region SetItemsPerPageMaximum
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(20)]
        [TestCase(100)]
        public void SetItemsPerPageMaximum_SetsConfigValue(int value) {
            Subject.SetItemsPerPageMaximum(value);
            Assert.That(Subject.Config.ItemsPerPageMaximum, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPageMaximum_ReturnsThisInstance() {
            Assert.That(Subject.SetItemsPerPageMaximum(1), Is.SameAs(Subject));
        }
        #endregion

        #region SetPageBaseZeroKey
        [TestCase("pbz")]
        [TestCase("PageBaseZero")]
        public void SetPageBaseZeroKey_SetsConfigValue(string value) {
            Subject.SetPageBaseZeroKey(value);
            Assert.That(Subject.Config.PageBaseZeroKey, Is.EqualTo(value));
        }

        [Test]
        public void SetPageBaseZeroKey_ReturnsThisInstance() {
            Assert.That(Subject.SetPageBaseZeroKey("pbz"), Is.SameAs(Subject));
        }
        #endregion

        #region SetItemsPerPageKey
        [TestCase("ipp")]
        [TestCase("ItemsPerPage")]
        public void SetItemsPerPageKey_SetsConfigValue(string value) {
            Subject.SetItemsPerPageKey(value);
            Assert.That(Subject.Config.ItemsPerPageKey, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPageKey_ReturnsThisInstance() {
            Assert.That(Subject.SetItemsPerPageKey("ipp"), Is.SameAs(Subject));
        }
        #endregion
    }
}
