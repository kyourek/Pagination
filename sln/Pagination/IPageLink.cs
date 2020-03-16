using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("2BB30611-2666-466E-A904-F977C467E0B3")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPageLink {
        [DispId(1)]
        bool IsPageRange { get; }

        [DispId(2)]
        bool IsRequestedPage { get; }

        [DispId(3)]
        int LinkPageBaseZero { get; }

        [DispId(4)]
        string LinkText { get; }

        [DispId(5)]
        int LowerPageBaseZero { get; }

        [DispId(6)]
        IPage Page { get; }

        [DispId(7)]
        int UpperPageBaseZero { get; }

        [DispId(8)]
        int LinkPageBaseOne { get; }

        [DispId(9)]
        int LowerPageBaseOne { get; }

        [DispId(10)]
        int UpperPageBaseOne { get; }
    }
}
