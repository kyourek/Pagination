using System.Linq;

namespace Pagination {
    public class PageWrapper : IPage {
        public IPage Page { get; }

        public PageWrapper(IPage page) {
            Page = page;
        }

        int IPage.PageBaseZero => Page.PageBaseZero;
        int IPage.PageBaseOne => Page.PageBaseOne;
        int IPage.PagesTotal => Page.PagesTotal;
        int IPage.ItemsPerPage => Page.ItemsPerPage;
        int IPage.ItemsTotal => Page.ItemsTotal;
        IQueryable IPage.Items => Page.Items;
        object IPage.State => Page.State;
        IPageConfig IPage.Config => Page.Config;
        IPageLinker IPage.Linker() => Page.Linker();
    }

    public class PageWrapper<TItem> : PageWrapper, IPage<TItem> {
        public new IPage<TItem> Page { get; }

        public PageWrapper(IPage<TItem> page) : base(page) {
            Page = page;
        }

        IQueryable<TItem> IPage<TItem>.Items => Page.Items;
    }

    public class PageWrapper<TItem, TState> : PageWrapper<TItem>, IPage<TItem, TState> {
        public new IPage<TItem, TState> Page { get; }

        public PageWrapper(IPage<TItem, TState> page) : base(page) {
            Page = page;
        }

        TState IPage<TItem, TState>.State => Page.State;
    }
}
