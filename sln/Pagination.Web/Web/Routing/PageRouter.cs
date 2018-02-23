using System;
using System.Web.Routing;

namespace Pagination.Web.Routing {
    class PageRouter {
        RouteValueDictionary GetRouteValues(object state, string itemsPerPageKey, string pageBaseZeroKey, int itemsPerPage, int pageBaseZero) {
            return new RouteValueDictionary(state) {
                [itemsPerPageKey] = itemsPerPage,
                [pageBaseZeroKey] = pageBaseZero
            };
        }

        public RouteValueDictionary GetRouteValues(PageContext context, object state, int? pageBaseZero, int? itemsPerPage) {
            if (null == context) throw new ArgumentNullException(nameof(context));
            var conf = context.Config ?? new PageConfig();
            var request = context.GetRequest() ?? new PageRequest();
            return GetRouteValues(
                state: state,
                itemsPerPage: itemsPerPage ?? request.ItemsPerPage ?? conf.ItemsPerPageDefault,
                pageBaseZero: pageBaseZero ?? request.PageBaseZero ?? 0,
                itemsPerPageKey: conf.ItemsPerPageKey,
                pageBaseZeroKey: conf.PageBaseZeroKey);
        }

        public RouteValueDictionary GetRouteValues(IPage page, int? pageBaseZero, int? itemsPerPage) {
            if (null == page) throw new ArgumentNullException(nameof(page));
            var conf = page.Config ?? new PageConfig();
            return GetRouteValues(
                state: page.State,
                itemsPerPage: itemsPerPage ?? page.ItemsPerPage,
                pageBaseZero: pageBaseZero ?? page.PageBaseZero,
                itemsPerPageKey: conf.ItemsPerPageKey,
                pageBaseZeroKey: conf.PageBaseZeroKey);
        }
    }
}
