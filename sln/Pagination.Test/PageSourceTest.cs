using NUnit.Framework;

namespace Pagination.Test {
    [TestFixture, TestOf(typeof(PageSource))]
    public class PageSourceTest {
        PageSource Subject {
            get => _Subject ?? (_Subject = new PageSource());
            set => _Subject = value;
        }
        PageSource _Subject;

        [SetUp]
        public void SetUp() {
            Subject = null;
        }

        [TestCase(1)]
        [TestCase(33)]
        public void SetItemsPerPage_SetsRequestValue(int value) {
            Subject.SetItemsPerPage(value);
            Assert.That(Subject.Request.ItemsPerPage, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPage_ReturnsInstance() {
            Assert.That(Subject.SetItemsPerPage(1), Is.SameAs(Subject));
        }

        [TestCase(1)]
        [TestCase(33)]
        public void SetPageBaseZero_SetsRequestValue(int value) {
            Subject.SetPageBaseZero(value);
            Assert.That(Subject.Request.PageBaseZero, Is.EqualTo(value));
        }

        [Test]
        public void SetPageBaseZero_ReturnsInstance() {
            Assert.That(Subject.SetPageBaseZero(1), Is.SameAs(Subject));
        }
    }
}
