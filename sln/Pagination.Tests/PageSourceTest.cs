using System.Linq;

using NUnit.Framework;

namespace Pagination.Tests {
    [TestFixture]
    public class PageSourceTest {
        PageSource _Subject;
        PageSource Subject => _Subject ?? (_Subject = new PageSource { Context = PageContext });

        PageContext _PageContext;
        PageContext PageContext => _PageContext ?? (_PageContext = new PageContext());

        [TearDown]
        public void TearDown() {
            _Subject = null;
            _PageContext = null;
        }

        [Test]
        public void DefaultItemsRequested_HasDefaultValue() {
            Assert.AreEqual(25, Subject.DefaultItemsRequested);
        }

        [Test]
        public void MaximumItemsRequested_HasDefaultValue() {
            Assert.AreEqual(100, Subject.MaximumItemsRequested);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(777)]
        public void PageRequested_CanBeSetArbitrarily(int value) {
            Subject.PageRequested = value;
            Assert.AreEqual(value, Subject.PageRequested);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void PageRequested_ComesFromContextIfNotSet(int value) {
            PageContext.AllValues[Subject.PageRequestedKey] = value;
            Assert.AreEqual(value, Subject.PageRequested);
        }

        [Test]
        public void PageRequested_ComesFromSetValueFirst() {
            PageContext.AllValues[Subject.PageRequestedKey] = 5;
            Subject.PageRequested = 1;
            Assert.AreEqual(1, Subject.PageRequested);
        }

        [Test]
        public void PageRequested_ReturnsToContextValueIfUnset() {
            PageContext.AllValues[Subject.PageRequestedKey] = 5;
            Subject.PageRequested = 1;
            Subject.PageRequested = null;
            Assert.AreEqual(5, Subject.PageRequested);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(777)]
        public void ItemsRequested_CanBeSetArbitrarily(int value) {
            Subject.ItemsRequested = value;
            Assert.AreEqual(value, Subject.ItemsRequested);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void ItemsRequested_ComesFromContextIfNotSet(int value) {
            PageContext.AllValues[Subject.ItemsRequestedKey] = value;
            Assert.AreEqual(value, Subject.ItemsRequested);
        }

        [Test]
        public void ItemsRequested_ComesFromSetValueFirst() {
            PageContext.AllValues[Subject.ItemsRequestedKey] = 5;
            Subject.ItemsRequested = 1;
            Assert.AreEqual(1, Subject.ItemsRequested);
        }

        [Test]
        public void ItemsRequested_ReturnsToContextValueIfUnset() {
            PageContext.AllValues[Subject.ItemsRequestedKey] = 5;
            Subject.ItemsRequested = 1;
            Subject.ItemsRequested = null;
            Assert.AreEqual(5, Subject.ItemsRequested);
        }

        [Test]
        public void PageRequestedKey_HasDefaultValue() {
            Assert.AreEqual("_PageRequested", Subject.PageRequestedKey);
        }

        [Test]
        public void ItemsRequestedKey_HasDefaultValue() {
            Assert.AreEqual("_ItemsRequested", Subject.ItemsRequestedKey);
        }

        [Test]
        public void FindPage_FindsFirstPageOfItems() {
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.Items;
            var expected = source.Take(Subject.DefaultItemsRequested);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
