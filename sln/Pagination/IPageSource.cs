using System.Linq;

namespace Pagination {
    public interface IPageSource {
        IPageConfig Config { get; }
        IPageRequest Request { get; }
        IPageSource SetItemsPerPage(int value);
        IPageSource SetPageBaseZero(int value);
    }

    public interface IPageSource<TItem> : IPageSource {
        IOrderedQueryable<TItem> ItemsSource { get; }
        IPage<TItem> FindPage();
        new IPageSource<TItem> SetItemsPerPage(int value);
        new IPageSource<TItem> SetPageBaseZero(int value);
    }

    public interface IPageSource<TItem, TFilter> : IPageSource<TItem> {
        TFilter Filter { get; }
        new IPage<TItem, TFilter> FindPage();
        new IPageSource<TItem, TFilter> SetItemsPerPage(int value);
        new IPageSource<TItem, TFilter> SetPageBaseZero(int value);
    }
}
