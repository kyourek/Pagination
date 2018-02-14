using System.Linq;

namespace Pagination {
    public class Page {
        public int PageRequested { get; }
        public int PageTotal { get; }
        public int ItemsRequested { get; }
        public int ItemsTotal { get; }
        public IQueryable Items { get; }
        public object Query { get; }

        public Page(int pageRequested, int pageTotal, int itemsRequested, int itemsTotal, IQueryable items, object query) {
            PageRequested = pageRequested;
            PageTotal = pageTotal;
            ItemsRequested = itemsRequested;
            ItemsTotal = itemsTotal;
            Items = items;
            Query = query;
        }
    }

    public class Page<TItem> : Page {
        public new IQueryable<TItem> Items { get; }

        public Page(int pageRequested, int pageTotal, int itemsRequested, int itemsTotal, IQueryable<TItem> items, object query) : base(pageRequested, pageTotal, itemsRequested, itemsTotal, items, query) {
            Items = items;
        }
    }

    public class Page<TItem, TQuery> : Page<TItem> {
        public new TQuery Query { get; }

        public Page(int pageRequested, int pageTotal, int itemsRequested, int itemsTotal, IQueryable<TItem> items, TQuery query) : base(pageRequested, pageTotal, itemsRequested, itemsTotal, items, query) {
            Query = query;
        }
    }
}
