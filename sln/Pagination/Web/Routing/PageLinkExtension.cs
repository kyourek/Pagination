using System;
using System.Web.Routing;

namespace Pagination.Web.Routing {
    public static class PageLinkExtension {
        public static RouteValueDictionary RouteValues(this IPageLink pageLink) {
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));

            var page = pageLink.Page;
            if (page == null) throw new ArgumentException(
                $"{nameof(pageLink.Page)} property of {nameof(pageLink)} cannot be null.", 
                nameof(pageLink));

            return page.RouteValues(pageLink.LinkPageBaseZero);
        }
    }
}
