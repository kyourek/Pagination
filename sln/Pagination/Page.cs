using System.Linq;

namespace Pagination {
    class Page : IPage {
        public int PageBaseZero { get; set; }
        public int PageTotal { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemsTotal { get; set; }
        public IQueryable Items { get; set; }
        public object Query { get; set; }
    }

    class Page<TItem> : Page, IPage<TItem> {
        public new IQueryable<TItem> Items { get; set; }
    }

    class Page<TItem, TQuery> : Page<TItem>, IPage<TItem, TQuery> {
        public new TQuery Query { get; set; }
    }
}
