using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Pagination.Web.Mvc {
    [TestFixture, TestOf(typeof(HtmlHelperExtension))]
    public class HtmlHelperExtensionTest {
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

        private class ViewDataContainer : IViewDataContainer {
            public ViewDataDictionary ViewData {
                get => _ViewData ?? (_ViewData = new ViewDataDictionary());
                set => _ViewData = value;
            }
            private ViewDataDictionary _ViewData;
        }

        private HtmlHelper Subject {
            get => _Subject ?? (_Subject = new HtmlHelper(new ViewContext { HttpContext = new HttpContext() }, new ViewDataContainer()));
            set => _Subject = value;
        }
        private HtmlHelper _Subject;

        [SetUp]
        public void SetUp() {
            Subject = null;
            Subject.RouteCollection.Clear();
        }
        
        [Test]
        public void PageLink_CreatesLinkForPage() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "page 2");
            var actual = HtmlHelperExtension.PageLink(Subject, pageLink).ToString();
            var expected = MvcHtmlString.Create("<a href=\"/homepage?Color=blue&amp;_p_ipp=54&amp;_p_pbz=2\">page 2</a>").ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PageLink_CreatesLinkForPageWithHtmlAttributes() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "page 2");
            var actual = HtmlHelperExtension.PageLink(Subject, pageLink, new Dictionary<string, object> { { "class", "biglink" } }).ToString();
            var expected = MvcHtmlString.Create("<a class=\"biglink\" href=\"/homepage?Color=blue&amp;_p_ipp=54&amp;_p_pbz=2\">page 2</a>").ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PageLink_ThrowsIfPageLinkIsNull() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "page 2");
            Assert.That(() =>
                HtmlHelperExtension.PageLink(Subject, null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("pageLink")
            );
        }

        [Test]
        public void PageLink_ThrowsIfHtmlHelperIsNull() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, State = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "page 2");
            Assert.That(() =>
                HtmlHelperExtension.PageLink(null, pageLink),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("htmlHelper")
            );
        }
    }
}
