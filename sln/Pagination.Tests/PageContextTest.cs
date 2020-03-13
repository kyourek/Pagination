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

        #region ReadPage
        [Test]
        public void ReadPage_FindsFirstPageOfItems() {
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
            var actual = page.Items;
            var expected = source.Take(Subject.Config.ItemsPerPageDefault);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void ReadPage_FindsNthPageOfItems(int n) {
            Subject.PageRequestFactory = () => new PageRequest { PageBaseZero = n };
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
            var actual = page.Items;
            var expected = source
                .Skip(n * Subject.Config.ItemsPerPageDefault)
                .Take(Subject.Config.ItemsPerPageDefault);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ReadPage_ConsidersItemsPerPageMaximum() {
            Subject.PageRequestFactory = () => new PageRequest { ItemsPerPage = 54321 };
            Subject.SetItemsPerPageMaximum(99);
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
            var actual = page.ItemsPerPage;
            var expected = 99;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ReadPage_SetsStateObject() {
            var state = new object();
            var page = Subject.ReadPage(new object[] { }.AsQueryable().OrderBy(obj => obj), state);
            Assert.That(page.State, Is.SameAs(state));
        }

        [Test]
        public void ReadPage_ReturnsNoItemsIfItemsSourceIsNull() {
            var page = Subject.ReadPage(default(IOrderedQueryable<string>));
            Assert.That(page.Items.Count(), Is.Zero);
        }

        [Test]
        public void ReadPage_SetsItemsTotalOfPage() {
            var source = Enumerable
                .Range(0, 8761)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
            Assert.That(page.ItemsTotal, Is.EqualTo(8761));
        }

        [TestCase(7250, 25)]
        public void ReadPage_SetsPagesTotalOfPage(int itemsTotal, int itemsPerPage) {
            Subject.PageRequestFactory = () => new PageRequest { ItemsPerPage = itemsPerPage };
            var source = Enumerable
                .Range(0, itemsTotal)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
            var actual = page.PagesTotal;
            var expected = (int)Math.Ceiling((double)itemsTotal / itemsPerPage);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ReadPage_SetsConfigOfPage() {
            var page = Subject.ReadPage(default(IOrderedQueryable<string>));
            Assert.That(page.Config, Is.SameAs(Subject.Config));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void ReadPage_SetsPageBaseZeroOfPage(int value) {
            Subject.PageRequestFactory = () => new PageRequest { PageBaseZero = value };
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
            Assert.That(page.PageBaseZero, Is.EqualTo(value));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void ReadPage_SetsPageBaseOneOfPage(int value) {
            Subject.PageRequestFactory = () => new PageRequest { PageBaseZero = value - 1 };
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.ReadPage(source);
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
