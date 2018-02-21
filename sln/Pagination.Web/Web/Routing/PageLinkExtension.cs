using System;
using System.Web.Routing;

namespace Pagination.Web.Routing {
    public static class PageLinkExtension {
        public static RouteValueDictionary RouteValues(this IPageLink pageLink) {
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            return PageExtension.RouteValues(pageLink.Page, pageLink.LinkPageBaseZero);
        }
    }
}
