using System.Web.Routing;

namespace Pagination.Web.Routing {
    public static class PageContextExtension {
        internal static PageRouter Router {
            get => _Router ?? (_Router = new PageRouter());
            set => _Router = value;
        }
        static PageRouter _Router;

        public static RouteValueDictionary RouteValues(this PageContext pageContext, object state, int? pageBaseZero = null, int? itemsPerPage = null) {
            return Router.GetRouteValues(pageContext, state, pageBaseZero, itemsPerPage);
        }
    }
}
