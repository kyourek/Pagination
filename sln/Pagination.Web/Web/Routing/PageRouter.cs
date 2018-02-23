using System;
using System.Web.Routing;

namespace Pagination.Web.Routing {
    class PageRouter {
        public RouteValueDictionary GetRouteValues(IPage page, int? pageBaseZero) {
            if (null == page) throw new ArgumentNullException(nameof(page));
            var conf = page.Config ?? new PageConfig();
            var state = page.State;
            var route = new RouteValueDictionary(state) {
                [conf.ItemsPerPageKey] = page.ItemsPerPage,
                [conf.PageBaseZeroKey] = pageBaseZero ?? page.PageBaseZero
            };
            return route;
        }
    }
}
