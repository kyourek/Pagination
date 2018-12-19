using System;
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
        [Obsolete("Use 'ReadPage' instead.")]
        IPage<TItem> FindPage();
        IPage<TItem> ReadPage();
        new IPageSource<TItem> SetItemsPerPage(int value);
        new IPageSource<TItem> SetPageBaseZero(int value);
    }

    public interface IPageSource<TItem, TState> : IPageSource<TItem> {
        TState State { get; }
        [Obsolete("Use 'ReadPage' instead.")]
        new IPage<TItem, TState> FindPage();
        new IPage<TItem, TState> ReadPage();
        new IPageSource<TItem, TState> SetItemsPerPage(int value);
        new IPageSource<TItem, TState> SetPageBaseZero(int value);
    }
}
