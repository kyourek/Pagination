using NUnit.Framework;

namespace Pagination.Web.Routing {
    [TestFixture, TestOf(typeof(PageContextExtension))]
    public class PageContextExtensionTest {
        PageContext Subject {
            get => _Subject ?? (_Subject = new PageContext());
            set => _Subject = value;
        }
        PageContext _Subject;

        [SetUp]
        public void SetUp() {
            Subject = null;
        }

        [Test]
        public void RouteValues_GetsStateValue() {
            var state = new { Width = 56 };
            var route = PageContextExtension.RouteValues(Subject, state);
            Assert.That(route["Width"], Is.EqualTo(56));
        }

        [Test]
        public void RouteValues_GetsItemsPerPageFromRequest() {
            Subject.GetRequest = () => new PageRequest { ItemsPerPage = 210 };
            var route = PageContextExtension.RouteValues(Subject, new object());
            Assert.That(route[Subject.Config.ItemsPerPageKey], Is.EqualTo(210));
        }

        [Test]
        public void RouteValues_GetsItemsPerPageFromDefault() {
            Subject.SetItemsPerPageDefault(97);
            var route = PageContextExtension.RouteValues(Subject, null);
            Assert.That(route[Subject.Config.ItemsPerPageKey], Is.EqualTo(97));
        }

        [Test]
        public void RouteValues_GetsItemsPerPageFromArgument() {
            var route = PageContextExtension.RouteValues(Subject, null, null, 19);
            Assert.That(route[Subject.Config.ItemsPerPageKey], Is.EqualTo(19));
        }

        [Test]
        public void RouteValues_GetsPageBaseZeroFromArgument() {
            var route = PageContextExtension.RouteValues(Subject, null, 456);
            Assert.That(route[Subject.Config.PageBaseZeroKey], Is.EqualTo(456));
        }

        [Test]
        public void RouteValues_GetsPageBaseZeroFromRequest() {
            Subject.GetRequest = () => new PageRequest { PageBaseZero = 9801 };
            var route = PageContextExtension.RouteValues(Subject, null);
            Assert.That(route[Subject.Config.PageBaseZeroKey], Is.EqualTo(9801));
        }

        [Test]
        public void RouteValues_GetsPageBaseZeroDefault() {
            var route = PageContextExtension.RouteValues(Subject, null);
            Assert.That(route[Subject.Config.PageBaseZeroKey], Is.EqualTo(0));
        }
    }
}
