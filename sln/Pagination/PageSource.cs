using System;
using System.Linq;

namespace Pagination {
    internal class PageSource : IPageSource {
        public IPageConfig Config {
            get => _Config ?? (_Config = new PageConfig());
            set => _Config = value;
        }
        private IPageConfig _Config;

        public PageRequest Request {
            get => _Request ?? (_Request = new PageRequest());
            set => _Request = value;
        }
        private PageRequest _Request;

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

    internal class PageSource<TItem> : PageSource, IPageSource<TItem> {
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
        private IOrderedQueryable<TItem> _Query;

        public IPage<TItem> ReadPage() =>
            ReadPage(default(object));

        public new IPageSource<TItem> SetItemsPerPage(int value) =>
            (IPageSource<TItem>)base.SetItemsPerPage(value);

        public new IPageSource<TItem> SetPageBaseZero(int value) =>
            (IPageSource<TItem>)base.SetPageBaseZero(value);
    }

    internal class PageSource<TItem, TState> : PageSource<TItem>, IPageSource<TItem, TState> {
        public TState State { get; set; }

        public new IPage<TItem, TState> ReadPage() =>
            ReadPage(State);

        public new IPageSource<TItem, TState> SetItemsPerPage(int value) =>
            (IPageSource<TItem, TState>)base.SetItemsPerPage(value);

        public new IPageSource<TItem, TState> SetPageBaseZero(int value) =>
            (IPageSource<TItem, TState>)base.SetPageBaseZero(value);
    }
}
