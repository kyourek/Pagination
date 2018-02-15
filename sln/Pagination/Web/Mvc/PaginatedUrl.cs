using System;
using System.Web.Mvc;

namespace Pagination.Web.Mvc {
    public static class PaginatedUrl {
        public static string Page(this UrlHelper urlHelper, IPageLink pageLink) {
            if (null == urlHelper) throw new ArgumentNullException(nameof(urlHelper));
            var actionName = Paginated.GetActionName(urlHelper);
            var routeValues = Paginated.GetRouteValues(pageLink);
            return urlHelper.Action(actionName, routeValues);
        }
    }
}
