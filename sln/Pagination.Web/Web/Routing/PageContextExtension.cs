#if NETCOREAPP
using Microsoft.AspNetCore.Routing;
#else
using System.Web.Routing;
#endif

namespace Pagination.Web.Routing {
    public static class PageContextExtension {
        internal static PageRouter Router {
            get => _Router ?? (_Router = new PageRouter());
            set => _Router = value;
        }
        private static PageRouter _Router;

        public static RouteValueDictionary RouteValues(this PageContext pageContext, object state, int? pageBaseZero = null, int? itemsPerPage = null) =>
            Router.GetRouteValues(pageContext, state, pageBaseZero, itemsPerPage);
    }
}
