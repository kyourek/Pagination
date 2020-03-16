using System.Linq;

namespace Pagination {
    internal class Page : IPage {
        public int PageBaseZero { get; set; }
        public int PagesTotal { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemsTotal { get; set; }
        public IQueryable Items { get; set; }
        public object State { get; set; }

        public int PageBaseOne => PageBaseZero + 1;

        public IPageConfig Config {
            get => _Config ?? (_Config = new PageConfig());
            set => _Config = value;
        }
        private IPageConfig _Config;

        public IPageLinker Linker() => new PageLinker(this);
    }

    internal class Page<TItem> : Page, IPage<TItem> {
        public new IQueryable<TItem> Items {
            get => (IQueryable<TItem>)base.Items;
            set => base.Items = value;
        }
    }

    internal class Page<TItem, TState> : Page<TItem>, IPage<TItem, TState> {
        public new TState State {
            get => (TState)base.State;
            set => base.State = value;
        }
    }
}
