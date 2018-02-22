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
            var src = ItemsSource;
            var req = Request;
            var cnf = Config;

            var def = cnf.ItemsPerPageDefault;
            var max = cnf.ItemsPerPageMaximum;
            var itemsPerPage = req.ItemsPerPage ?? def;
            if (itemsPerPage < 1) itemsPerPage = def;
            if (itemsPerPage > max) itemsPerPage = max;

            var itemsTotal = src.Count();
            var pagesTotal = (int)Math.Ceiling((double)itemsTotal / itemsPerPage);

            var min = 0;
            var pageBaseZero = req.PageBaseZero ?? min;
            if (pageBaseZero < 0) pageBaseZero = min;

            var items = src
                .Skip(itemsPerPage * pageBaseZero)
                .Take(itemsPerPage);

            return new Page<TItem, TFilter> {
                Config = cnf,
                Items = items,
                ItemsPerPage = itemsPerPage,
                ItemsTotal = itemsTotal,
                PageBaseZero = pageBaseZero,
                PagesTotal = pagesTotal,
                Filter = filter
            };
        }
        public IOrderedQueryable<TItem> ItemsSource {
            get => _Query ?? (_Query = new TItem[] { }.AsQueryable().OrderBy(item => item));
            set => _Query = value;
        }
        IOrderedQueryable<TItem> _Query;

        public IPage<TItem> FindPage() {
            return FindPage(default(object));
        }
    }

    class PageSource<TItem, TFilter> : PageSource<TItem>, IPageSource<TItem, TFilter> {
        public TFilter Filter { get; set; }

        public new IPage<TItem, TFilter> FindPage() {
            return FindPage(Filter);
        }
    }
}
