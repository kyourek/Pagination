using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

using NUnit.Framework;

namespace Pagination.Web.Mvc {
    [TestFixture, TestOf(typeof(HtmlHelperExtension))]
    public class HtmlHelperExtensionTest {
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

        class ViewDataContainer : IViewDataContainer {
            ViewDataDictionary _ViewData;
            public ViewDataDictionary ViewData {
                get => _ViewData ?? (_ViewData = new ViewDataDictionary());
                set => _ViewData = value;
            }
        }

        HtmlHelper Subject {
            get => _Subject ?? (_Subject = new HtmlHelper(new ViewContext(), new ViewDataContainer()));
            set => _Subject = value;
        }
        HtmlHelper _Subject;

        [SetUp]
        public void SetUp() {
            Subject.ViewContext.HttpContext = new HttpContext();
            Subject.RouteCollection.Clear();
        }

        [TearDown]
        public void TearDown() {
            Subject = null;
        }

        [Test]
        public void PageLink_CreatesLinkForPage() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, Filter = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "page 2");
            var actual = HtmlHelperExtension.PageLink(Subject, pageLink).ToString();
            var expected = MvcHtmlString.Create("<a href=\"/homepage?Color=blue&amp;_p_ipp=54&amp;_p_pbz=2\">page 2</a>").ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PageLink_CreatesLinkForPageWithHtmlAttributes() {
            Subject.RouteCollection.MapRoute("Test", "homepage");
            var page = new Page { ItemsPerPage = 54, Filter = new { Color = "blue" } };
            var pageLink = new PageLink(page, 2, "page 2");
            var actual = HtmlHelperExtension.PageLink(Subject, pageLink, new Dictionary<string, object> { { "class", "biglink" } }).ToString();
            var expected = MvcHtmlString.Create("<a class=\"biglink\" href=\"/homepage?Color=blue&amp;_p_ipp=54&amp;_p_pbz=2\">page 2</a>").ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
