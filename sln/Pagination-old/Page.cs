using System.Linq;

namespace Pagination {
    class Page : IPage {
        public int PageBaseZero { get; set; }
        public int PageTotal { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemsTotal { get; set; }
        public IQueryable Items { get; set; }
        public object Query { get; set; }

        public int PageBaseOne => PageBaseZero + 1;

        public IPageConfig Config {
            get => _Config ?? (_Config = new PageConfig());
            set => _Config = value;
        }
        IPageConfig _Config;

        public IPageRequest Request {
            get => _Request ?? (_Request = new PageRequest());
            set => _Request = value;
        }
        IPageRequest _Request;

        public class Of<TItem> : Page, IPage<TItem> {
            public new IQueryable<TItem> Items {
                get => (IQueryable<TItem>)base.Items;
                set => base.Items = value;
            }

            public class From<TQuery> : Of<TItem>, IPage<TItem, TQuery> {
                public new TQuery Query {
                    get => (TQuery)base.Query;
                    set => base.Query = value;
                }
            }
        }
    }
}
