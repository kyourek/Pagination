using NUnit.Framework;

namespace Pagination {
    [TestFixture, TestOf(typeof(PageChain))]
    public class PageChainTest {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(123)]
        public void Constructor_SetsItemsTotal(int value) {
            var subject = new PageChain(value, 1, null);
            Assert.That(subject.ItemsTotal, Is.EqualTo(value));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(123)]
        public void Constructor_SetsPageTotal(int value) {
            var subject = new PageChain(1000, value, null);
            Assert.That(subject.PageTotal, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_SetsLinks() {
            var links = new IPageLink[] { };
            var subject = new PageChain(1, 1, links);
            Assert.That(links, Is.SameAs(links));
        }

        [Test]
        public void Constructor_LinksCannotBeNull() {
            var subject = new PageChain(1, 1, null);
            Assert.That(subject.Links, Is.Not.Null);
        }
    }
}
