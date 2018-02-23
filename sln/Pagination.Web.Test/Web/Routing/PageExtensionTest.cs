using NUnit.Framework;

namespace Pagination.Web.Routing {
    [TestFixture, TestOf(typeof(PageExtension))]
    public class PageExtensionTest {
        [TestCase("Width", 38)]
        [TestCase("Type", "Appliance")]
        [TestCase("_p_ipp", 12)]
        [TestCase("_p_pbz", 511)]
        public void RouteValues_SetsPropertyValue(string name, object value) {
            var page = new Page {
                ItemsPerPage = 12,
                PageBaseZero = 511,
                State = new {
                    Width = 38,
                    Type = "Appliance"
                }
            };

            var routeValues = PageExtension.RouteValues(page);
            var actual = routeValues[name];
            var expected = value;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RouteValues_UsesSpecifiedPage() {
            var page = new Page {
                ItemsPerPage = 12,
                PageBaseZero = 511,
                State = new {
                    Width = 38,
                    Type = "Appliance"
                }
            };

            var routeValues = PageExtension.RouteValues(page, 622);
            var actual = routeValues["_p_pbz"];
            var expected = 622;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RouteValues_ThrowsIfPageIsNull() {
            Assert.That(() =>
                PageExtension.RouteValues(null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("page"));
        }
    }
}
