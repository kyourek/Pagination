using NUnit.Framework;

namespace Pagination.Tests {
    [TestFixture]
    public class PageFactoryTest {
        PageFactory Subject;

        [SetUp]
        public void SetUp() {
            Subject = new PageFactory();
        }

        [Test]
        public void DefaultItemsRequested_IsDefaultValue() {
            Assert.AreEqual(25, Subject.DefaultItemsRequested);
        }

        [Test]
        public void MaximumItemsRequested_IsDefaultValue() {
            Assert.AreEqual(100, Subject.MaximumItemsRequested);
        }
    }
}
