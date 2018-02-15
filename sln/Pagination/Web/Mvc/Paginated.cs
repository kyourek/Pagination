using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pagination.Web.Mvc {
    class Paginated {
        private static string GetRouteValue(RouteValueDictionary values, string key) {
            if (null == values) return null;
            var action = values[key];
            return action == null ? null : action.ToString();
        }

        private static string GetRouteValue(RouteData routeData, string key) {
            return GetRouteValue(routeData == null ? null : routeData.Values, key);
        }

        private static string GetRouteValue(RequestContext requestContext, string key) {
            return GetRouteValue(requestContext == null ? null : requestContext.RouteData, key);
        }

        private static string GetRouteValue(UrlHelper urlHelper, string key) {
            if (null == urlHelper) throw new ArgumentNullException("urlHelper");
            return GetRouteValue(urlHelper.RequestContext, key);
        }

        private static string GetRouteValue(ViewContext viewContext, string key) {
            return GetRouteValue(viewContext == null ? null : viewContext.RequestContext, key);
        }

        private static string GetRouteValue(HtmlHelper htmlHelper, string key) {
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            return GetRouteValue(htmlHelper.ViewContext, key);
        }

        public static string GetActionName(UrlHelper urlHelper) {
            return GetRouteValue(urlHelper, "action");
        }

        public static string GetActionName(HtmlHelper htmlHelper) {
            return GetRouteValue(htmlHelper, "action");
        }

        public static RouteValueDictionary GetRouteValues(IPageLink pageLink) {
            var obj = pageLink?.Page?.QueryPage(pageLink?.LinkPage);
            return new RouteValueDictionary(obj);
        }
    }
}
