using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Pagination.Web.Mvc {
    using Routing;

    public static class HtmlHelperExtension {
        static string ActionName(this HtmlHelper htmlHelper) {
            if (null == htmlHelper) throw new ArgumentNullException(nameof(htmlHelper));
            return htmlHelper.ViewContext?.RouteData?.Values?["action"]?.ToString();
        }

        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLink pageLink, IDictionary<string, object> htmlAttributes) {
            if (null == htmlHelper) throw new ArgumentNullException(nameof(htmlHelper));
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            var actionName = htmlHelper.ActionName();
            var routeValues = pageLink.RouteValues();
            return htmlHelper.ActionLink(pageLink.LinkText, actionName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLink pageLink) {
            if (null == htmlHelper) throw new ArgumentNullException(nameof(htmlHelper));
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            var actionName = htmlHelper.ActionName();
            var routeValues = pageLink.RouteValues();
            return htmlHelper.ActionLink(pageLink.LinkText, actionName, routeValues);
        }
    }
}
