using NUnit.Framework;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pagination.Web.Mvc {
    [TestFixture, TestOf(typeof(UrlHelperExtension))]
    public class UrlHelperExtensionTest {
        private class HttpRequest : HttpRequestBase {
            public override string ApplicationPath => "/";

            public override NameValueCollection ServerVariables => 
                _ServerVariables ?? (
                _ServerVariables = new NameValueCollection());
            private NameValueCollection _ServerVariables;
        }

        private class HttpResponse : HttpResponseBase {
            public override string ApplyAppPathModifier(string virtualPath) =>
                virtualPath;
        }

        private class HttpContext : HttpContextBase {
            public override HttpRequestBase Request => 
                _Request ?? (
                _Request = new HttpRequest());
            private HttpRequestBase _Request;

            public override HttpResponseBase Response => 
                _Response ?? (
                _Response = new HttpResponse());
            private HttpResponseBase _Response;
        }

        private UrlHelper Subject {
            get => _Subject ?? (_Subject = new UrlHelper(new RequestContext(new HttpContext(), new RouteData())));
            set => _Subject = value;
        }
        private UrlHelper _Subject;

        [SetUp]
        public void SetUp() {
            Subject = null;
            Subject.RouteCollection.Clear();
        }

        [Test]
        public void PageLink_CreatesUrlForPage() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "2");
            var actual = UrlHelperExtension.PageLink(Subject, pageLink);
            var expected = "/homepage?Color=blue&_p_ipp=54&_p_pbz=2";
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PageLink_ThrowsIfPageLinkIsNull() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "2");
            Assert.That(() =>
                UrlHelperExtension.PageLink(Subject, null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("pageLink")
            );
        }

        [Test]
        public void PageLink_ThrowsIfUrlHelperIsNull() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "2");
            Assert.That(() =>
                UrlHelperExtension.PageLink(null, pageLink),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("urlHelper")
            );
        }
    }
}
