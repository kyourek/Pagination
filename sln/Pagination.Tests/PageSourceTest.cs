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

        [Test]
        public void SetItemsPerPage_ReturnsInstanceOfGenericType() {
            var subject = new PageSource<string>();
            Assert.That(subject.SetItemsPerPage(12), Is.SameAs(subject));
        }

        [Test]
        public void SetItemsPerPage_ReturnsInstanceOfGenericType2() {
            var subject = new PageSource<string, object>();
            Assert.That(subject.SetItemsPerPage(12), Is.SameAs(subject));
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

        [Test]
        public void SetPageBaseZero_ReturnsInstanceOfGenericType() {
            var subject = new PageSource<string>();
            Assert.That(subject.SetPageBaseZero(5), Is.SameAs(subject));
        }

        [Test]
        public void SetPageBaseZero_ReturnsInstanceOfGenericType2() {
            var subject = new PageSource<string, object>();
            Assert.That(subject.SetPageBaseZero(5), Is.SameAs(subject));
        }
    }
}
