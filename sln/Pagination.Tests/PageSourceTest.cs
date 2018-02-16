using System.Linq;

using NUnit.Framework;

namespace Pagination.Tests {
    [TestFixture, TestOf(typeof(PageSource))]
    public class PageSourceTest {
        PageSource _Subject;
        PageSource Subject => _Subject ?? (_Subject = new PageSource());

        [TearDown]
        public void TearDown() {
            _Subject = null;
        }

        [Test]
        public void ItemsPerPageDefault_HasDefaultValue() {
            Assert.AreEqual(25, Subject.ItemsPerPageDefault);
        }

        [Test]
        public void ItemsPerPageMaximum_HasDefaultValue() {
            Assert.AreEqual(100, Subject.ItemsPerPageMaximum);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(777)]
        public void PageBaseZero_CanBeSetArbitrarily(int value) {
            Subject.PageBaseZero = value;
            Assert.AreEqual(value, Subject.PageBaseZero);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(777)]
        public void ItemsPerPage_CanBeSetArbitrarily(int value) {
            Subject.ItemsPerPage = value;
            Assert.AreEqual(value, Subject.ItemsPerPage);
        }

        [Test]
        public void FindPage_FindsFirstPageOfItems() {
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.Items;
            var expected = source.Take(Subject.ItemsPerPageDefault);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(11)]
        public void FindPage_FinsNthPageOfItems(int n) {
            Subject.PageBaseZero = n;
            var source = Enumerable
                .Range(0, 1000)
                .AsQueryable()
                .OrderBy(item => item);
            var page = Subject.FindPage(source);
            var actual = page.Items;
            var expected = source
                .Skip(n * Subject.ItemsPerPageDefault)
                .Take(Subject.ItemsPerPageDefault);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
