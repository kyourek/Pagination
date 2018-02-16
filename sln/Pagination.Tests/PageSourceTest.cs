using System.Linq;

using NUnit.Framework;

namespace Pagination.Tests {
    [TestFixture, TestOf(typeof(PageSource))]
    public class PageSourceTest {
        PageSource _Subject;
        PageSource Subject => _Subject ?? (_Subject = new PageSource());

        PageConfig PageConfig => (PageConfig)Subject.Config;
        PageRequest PageRequest => (PageRequest)Subject.Request;

        [TearDown]
        public void TearDown() {
            _Subject = null;
        }

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
        public void FindPage_FinsNthPageOfItems(int n) {
            PageRequest.PageBaseZero = n;
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
    }
}
