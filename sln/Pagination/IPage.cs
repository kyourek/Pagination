using System.Linq;
using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("28BC0E0E-EAAD-42DD-8DC8-257588F99D63")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPage {
        [DispId(1)]
        int PageBaseZero { get; }

        [DispId(2)]
        int PageBaseOne { get; }

        [DispId(3)]
        int PagesTotal { get; }

        [DispId(4)]
        int ItemsPerPage { get; }

        [DispId(5)]
        int ItemsTotal { get; }

        [DispId(6)]
        object State { get; }

        [DispId(7)]
        IPageConfig Config { get; }

        [DispId(8)]
        IPageLinker Linker();

        [ComVisible(false)]
        IQueryable Items { get; }
    }

    public interface IPage<TItem> : IPage {
        new IQueryable<TItem> Items { get; }
    }

    public interface IPage<TItem, TState> : IPage<TItem> {
        new TState State { get; }
    }
}
