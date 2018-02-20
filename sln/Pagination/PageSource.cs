using System;
using System.Linq;

namespace Pagination {
    class PageSource : IPageSource {
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
    }

    class PageSource<TItem> : PageSource, IPageSource<TItem> {
        protected IPage<TItem, TFilter> FindPage<TFilter>(TFilter filter) {
            var def = default(int);
            var qry = Query;
            var req = Request;
            var conf = Config;

            def = conf.ItemsPerPageDefault;
            var itemsPerPage = req.ItemsPerPage ?? def;
            if (itemsPerPage < 1) itemsPerPage = def;
            if (itemsPerPage > conf.ItemsPerPageMaximum) itemsPerPage = def;

            var totalItems = qry.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            def = 0;
            var pageBaseZero = req.PageBaseZero ?? def;
            if (pageBaseZero < 0) pageBaseZero = def;

            var items = qry
                .Skip(itemsPerPage * pageBaseZero)
                .Take(itemsPerPage);

            return new Page<TItem, TFilter> {
                Config = conf,
                Items = items,
                ItemsPerPage = itemsPerPage,
                ItemsTotal = totalItems,
                PageBaseZero = pageBaseZero,
                PageTotal = totalPages,
                Filter = filter,
                Request = req
            };
        }
        public IOrderedQueryable<TItem> Query {
            get => _Query ?? (_Query = new TItem[] { }.AsQueryable().OrderBy(item => item));
            set => _Query = value;
        }
        IOrderedQueryable<TItem> _Query;

        public IPage<TItem> FindPage() {
            return FindPage(default(object));
        }
    }

    class PageSource<TItem, TFilter> : PageSource<TItem>, IPageSource<TItem, TFilter> {
        public TFilter Filter {
            get => _Filter;
            set => _Filter = value;
        }
        TFilter _Filter;

        public new IPage<TItem, TFilter> FindPage() {
            return FindPage(Filter);
        }
    }
}
