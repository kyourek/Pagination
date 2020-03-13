using System;

#if NETCOREAPP
using Microsoft.AspNetCore.Routing;
#else
using System.Web.Routing;
#endif

namespace Pagination.Web.Routing {
    public static class PageLinkExtension {
        public static RouteValueDictionary RouteValues(this IPageLink pageLink) {
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            return PageExtension.RouteValues(pageLink.Page, pageLink.LinkPageBaseZero);
        }
    }
}
