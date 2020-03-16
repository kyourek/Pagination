using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("A63B8679-46CE-404C-AF94-BF699697C8CA")]
    [ComVisible(true)]
#if NETCOREAPP
    [ClassInterface(ClassInterfaceType.None)]
#else
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
#endif
    public class PageWrapper : IPage {
        public IPage Page { get; }

        public PageWrapper(IPage page) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
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
            Page = page ?? throw new ArgumentNullException(nameof(page));
        }

        IQueryable<TItem> IPage<TItem>.Items => Page.Items;
    }

    public class PageWrapper<TItem, TState> : PageWrapper<TItem>, IPage<TItem, TState> {
        public new IPage<TItem, TState> Page { get; }

        public PageWrapper(IPage<TItem, TState> page) : base(page) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
        }

        TState IPage<TItem, TState>.State => Page.State;
    }
}
