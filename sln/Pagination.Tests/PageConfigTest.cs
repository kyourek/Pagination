using NUnit.Framework;

namespace Pagination {
    [TestFixture, TestOf(typeof(PageConfig))]
    public class PageConfigTest {
        PageConfig Subject => _Subject ?? (_Subject = new PageConfig());
        PageConfig _Subject;

        [TearDown]
        public void TearDown() {
            _Subject = null;
        }

        [Test]
        public void ItemsPerPageDefault_HasDefaultValue() {
            Assert.That(Subject.ItemsPerPageDefault, Is.EqualTo(25));
        }

        [Test]
        public void ItemsPerPageMaximum_HasDefaultValue() {
            Assert.That(Subject.ItemsPerPageMaximum, Is.EqualTo(100));
        }

        [TestCase(19)]
        public void ItemsPerPageDefault_CanBeSet(int value) {
            Subject.ItemsPerPageDefault = value;
            Assert.That(Subject.ItemsPerPageDefault, Is.EqualTo(value));
        }

        [TestCase(1245)]
        public void ItemsPerPageMaximum_CanBeSet(int value) {
            Subject.ItemsPerPageMaximum = value;
            Assert.That(Subject.ItemsPerPageMaximum, Is.EqualTo(value));
        }

        [Test]
        public void ItemsPerPageKey_HasDefaultValue() {
            Assert.That(Subject.ItemsPerPageKey, Is.EqualTo("_p_ipp"));
        }

        [Test]
        public void PageBaseZeroKey_HasDefaultValue() {
            Assert.That(Subject.PageBaseZeroKey, Is.EqualTo("_p_pbz"));
        }

        [TestCase("ItemsPerPage")]
        public void ItemsPerPageKey_CanBeSet(string value) {
            Subject.ItemsPerPageKey = value;
            Assert.That(Subject.ItemsPerPageKey, Is.EqualTo(value));
        }

        [TestCase("PageBaseZero")]
        public void PageBaseZeroKey_CanBeSet(string value) {
            Subject.PageBaseZeroKey = value;
            Assert.That(Subject.PageBaseZeroKey, Is.EqualTo(value));
        }

        [Test]
        public void ItemsPerPageKey_ReturnsToDefaultIfSetToNull() {
            Subject.ItemsPerPageKey = null;
            Assert.That(Subject.ItemsPerPageKey, Is.EqualTo("_p_ipp"));
        }

        [Test]
        public void PageBaseZeroKey_ReturnsToDefaultIfSetToNull() {
            Subject.PageBaseZeroKey = null;
            Assert.That(Subject.PageBaseZeroKey, Is.EqualTo("_p_pbz"));
        }
    }
}
