using System.Linq;
using System.Web.Routing;

namespace Pagination {
    class Page : IPage {
        public int PageRequested { get; set; }
        public int PageTotal { get; set; }
        public int ItemsRequested { get; set; }
        public int ItemsTotal { get; set; }
        public IQueryable Items { get; set; }
        public object Query { get; set; }
        public string PageRequestedKey { get; set; }
        public string ItemsRequestedKey { get; set; }

        public object QueryPage(int? pageRequested = null) {
            pageRequested = pageRequested ?? PageRequested;
            var dict = new RouteValueDictionary(Query);
            dict[PageRequestedKey] = pageRequested;
            dict[ItemsRequestedKey] = ItemsRequested;
            return dict;
        }
    }

    class Page<TItem> : Page, IPage<TItem> {
        public new IQueryable<TItem> Items { get; set; }
    }

    class Page<TItem, TQuery> : Page<TItem>, IPage<TItem, TQuery> {
        public new TQuery Query { get; set; }
    }
}
