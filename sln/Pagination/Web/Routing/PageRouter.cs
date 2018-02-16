using System;
using System.Web.Routing;

namespace Pagination.Web.Routing {
    class PageRouter {
        public string ItemsPerPageKey {
            get => _ItemsPerPageKey ?? (_ItemsPerPageKey = HttpPageConfig.Default.ItemsPerPageKey);
            set => _ItemsPerPageKey = value;
        }
        string _ItemsPerPageKey;

        public string PageBaseZeroKey {
            get => _PageBaseZeroKey ?? (_PageBaseZeroKey = HttpPageConfig.Default.PageBaseZeroKey);
            set => _PageBaseZeroKey = value;
        }
        string _PageBaseZeroKey;

        public RouteValueDictionary GetRouteValues(IPage page, int? pageBaseZero) {
            if (null == page) throw new ArgumentNullException(nameof(page));
            var query = page.Query;
            var route = new RouteValueDictionary(query) {
                [ItemsPerPageKey] = page.ItemsPerPage,
                [PageBaseZeroKey] = pageBaseZero ?? page.PageBaseZero
            };
            return route;
        }
    }
}
