using System;
using System.Linq;

namespace Pagination {
    class PageSource : IPageSource {
        public IPageConfig Config {
            get => _Config ?? (_Config = new PageConfig());
            set => _Config = value;
        }
        IPageConfig _Config;

        public PageRequest Request {
            get => _Request ?? (_Request = new PageRequest());
            set => _Request = value;
        }
        PageRequest _Request;

        public IPageSource SetItemsPerPage(int value) {
            Request.ItemsPerPage = value;
            return this;
        }

        public IPageSource SetPageBaseZero(int value) {
            Request.PageBaseZero = value;
            return this;
        }

        IPageRequest IPageSource.Request => Request;
    }

    class PageSource<TItem> : PageSource, IPageSource<TItem> {
        protected IPage<TItem, TState> ReadPage<TState>(TState state) {
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

            return new Page<TItem, TState> {
                Config = cnf,
                Items = items,
                ItemsPerPage = itemsPerPage,
                ItemsTotal = itemsTotal,
                PageBaseZero = pageBaseZero,
                PagesTotal = pagesTotal,
                State = state
            };
        }

        public IOrderedQueryable<TItem> ItemsSource {
            get => _Query ?? (_Query = new TItem[] { }.AsQueryable().OrderBy(item => item));
            set => _Query = value;
        }
        IOrderedQueryable<TItem> _Query;

        [Obsolete("Use 'ReadPage' instead.")]
        public IPage<TItem> FindPage() {
            return ReadPage();
        }

        public IPage<TItem> ReadPage() {
            return ReadPage(default(object));
        }

        public new IPageSource<TItem> SetItemsPerPage(int value) {
            return (IPageSource<TItem>)base.SetItemsPerPage(value);
        }

        public new IPageSource<TItem> SetPageBaseZero(int value) {
            return (IPageSource<TItem>)base.SetPageBaseZero(value);
        }
    }

    class PageSource<TItem, TState> : PageSource<TItem>, IPageSource<TItem, TState> {
        public TState State { get; set; }

        [Obsolete("Use 'ReadPage' instead.")]
        public new IPage<TItem, TState> FindPage() {
            return ReadPage();
        }

        public new IPage<TItem, TState> ReadPage() {
            return ReadPage(State);
        }

        public new IPageSource<TItem, TState> SetItemsPerPage(int value) {
            return (IPageSource<TItem, TState>)base.SetItemsPerPage(value);
        }

        public new IPageSource<TItem, TState> SetPageBaseZero(int value) {
            return (IPageSource<TItem, TState>)base.SetPageBaseZero(value);
        }
    }
}
