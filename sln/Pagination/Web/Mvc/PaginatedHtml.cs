using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Pagination.Web.Mvc {
    public static class PaginatedHtml {
        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLink pageLink, object htmlAttributes) {
            if (null == pageLink) throw new ArgumentNullException(nameof(PageLink));
            if (null == htmlHelper) throw new ArgumentNullException(nameof(htmlHelper));
            var actionName = Paginated.GetActionName(htmlHelper);
            var routeValues = Paginated.GetRouteValues(pageLink);
            return htmlHelper.ActionLink(pageLink.LinkText, actionName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLink pageLink, IDictionary<string, object> htmlAttributes) {
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            if (null == htmlHelper) throw new ArgumentNullException(nameof(HtmlHelper));
            var actionName = Paginated.GetActionName(htmlHelper);
            var routeValues = Paginated.GetRouteValues(pageLink);
            return htmlHelper.ActionLink(pageLink.LinkText, actionName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLink pageLink) {
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            if (null == htmlHelper) throw new ArgumentNullException(nameof(HtmlHelper));
            var actionName = Paginated.GetActionName(htmlHelper);
            var routeValues = Paginated.GetRouteValues(pageLink);
            return htmlHelper.ActionLink(pageLink.LinkText, actionName, routeValues);
        }
    }
}
