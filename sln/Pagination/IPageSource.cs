using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("D20AA80F-B1C8-4893-AB28-C18E7FA0539B")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPageSource {
        [DispId(1)]
        IPageConfig Config { get; }

        [DispId(2)]
        IPageRequest Request { get; }
        
        [DispId(3)]
        IPageSource SetItemsPerPage(int value);

        [DispId(4)]
        IPageSource SetPageBaseZero(int value);
    }

    public interface IPageSource<TItem> : IPageSource {
        IOrderedQueryable<TItem> ItemsSource { get; }
        IPage<TItem> ReadPage();
        new IPageSource<TItem> SetItemsPerPage(int value);
        new IPageSource<TItem> SetPageBaseZero(int value);
    }

    public interface IPageSource<TItem, TState> : IPageSource<TItem> {
        TState State { get; }
        new IPage<TItem, TState> ReadPage();
        new IPageSource<TItem, TState> SetItemsPerPage(int value);
        new IPageSource<TItem, TState> SetPageBaseZero(int value);
    }
}
