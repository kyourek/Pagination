using System;

#if NETCOREAPP
using Microsoft.AspNetCore.Routing;
#else
using System.Web.Routing;
#endif

namespace Pagination.Web.Routing {
    internal class PageRouter {
        private static RouteValueDictionary GetRouteValues(object state, string itemsPerPageKey, string pageBaseZeroKey, int itemsPerPage, int pageBaseZero) =>
            new RouteValueDictionary(state) {
                [itemsPerPageKey] = itemsPerPage,
                [pageBaseZeroKey] = pageBaseZero
            };

        public RouteValueDictionary GetRouteValues(PageContext context, object state, int? pageBaseZero, int? itemsPerPage) {
            if (null == context) throw new ArgumentNullException(nameof(context));
            var config = context.Config ?? new PageConfig();
            var request = context.PageRequestFactory() ?? new PageRequest();
            return GetRouteValues(
                state: state,
                itemsPerPage: itemsPerPage ?? request.ItemsPerPage ?? config.ItemsPerPageDefault,
                pageBaseZero: pageBaseZero ?? request.PageBaseZero ?? 0,
                itemsPerPageKey: config.ItemsPerPageKey,
                pageBaseZeroKey: config.PageBaseZeroKey);
        }

        public RouteValueDictionary GetRouteValues(IPage page, int? pageBaseZero, int? itemsPerPage) {
            if (null == page) throw new ArgumentNullException(nameof(page));
            var config = page.Config ?? new PageConfig();
            return GetRouteValues(
                state: page.State,
                itemsPerPage: itemsPerPage ?? page.ItemsPerPage,
                pageBaseZero: pageBaseZero ?? page.PageBaseZero,
                itemsPerPageKey: config.ItemsPerPageKey,
                pageBaseZeroKey: config.PageBaseZeroKey);
        }
    }
}
