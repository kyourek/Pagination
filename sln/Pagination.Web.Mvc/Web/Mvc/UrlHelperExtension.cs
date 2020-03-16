using System;
using System.Web.Mvc;

namespace Pagination.Web.Mvc {
    using Routing;

    public static class UrlHelperExtension {
        private static string ActionName(this UrlHelper urlHelper) {
            if (null == urlHelper) throw new ArgumentNullException(nameof(urlHelper));
            return urlHelper.RequestContext?.RouteData?.Values?["action"]?.ToString();
        }

        public static string PageLink(this UrlHelper urlHelper, IPageLink pageLink) {
            if (null == urlHelper) throw new ArgumentNullException(nameof(urlHelper));
            if (null == pageLink) throw new ArgumentNullException(nameof(pageLink));
            var actionName = urlHelper.ActionName();
            var routeValues = pageLink.RouteValues();
            return urlHelper.Action(actionName, routeValues);
        }
    }
}
