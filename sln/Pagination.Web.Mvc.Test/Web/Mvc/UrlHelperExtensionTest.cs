using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using NUnit.Framework;

namespace Pagination.Web.Mvc {
    [TestFixture, TestOf(typeof(UrlHelperExtension))]
    public class UrlHelperExtensionTest {
        class HttpRequest : HttpRequestBase {
            NameValueCollection _ServerVariables;
            public override string ApplicationPath => "/";
            public override NameValueCollection ServerVariables => _ServerVariables ?? (_ServerVariables = new NameValueCollection());
        }

        class HttpResponse : HttpResponseBase {
            public override string ApplyAppPathModifier(string virtualPath) {
                return virtualPath;
            }
        }

        class HttpContext : HttpContextBase {
            HttpRequestBase _Request;
            HttpResponseBase _Response;
            public override HttpRequestBase Request => _Request ?? (_Request = new HttpRequest());
            public override HttpResponseBase Response => _Response ?? (_Response = new HttpResponse());
        }

        UrlHelper Subject {
            get => _Subject ?? (_Subject = new UrlHelper(new RequestContext(new HttpContext(), new RouteData())));
            set => _Subject = value;
        }
        UrlHelper _Subject;

        [SetUp]
        public void SetUp() {
            Subject.RouteCollection.Clear();
        }

        [TearDown]
        public void TearDown() {
            Subject = null;
        }

        [Test]
        public void PageLink_CreatesUrlForPage() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, Filter = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "2");
            var actual = UrlHelperExtension.PageLink(Subject, pageLink);
            var expected = "/homepage?Color=blue&_p_ipp=54&_p_pbz=2";
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PageLink_ThrowsIfPageLinkIsNull() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, Filter = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "2");
            Assert.That(() =>
                UrlHelperExtension.PageLink(Subject, null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("pageLink")
            );
        }

        [Test]
        public void PageLink_ThrowsIfUrlHelperIsNull() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, Filter = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "2");
            Assert.That(() =>
                UrlHelperExtension.PageLink(null, pageLink),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("urlHelper")
            );
        }
    }
}
