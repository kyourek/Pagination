using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("F0A6D7A2-3189-4C56-93D8-500591FBAD83")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPageConfig {
        [DispId(1)]
        int ItemsPerPageDefault { get; }

        [DispId(2)]
        string ItemsPerPageKey { get; }
        
        [DispId(3)]
        int ItemsPerPageMaximum { get; }

        [DispId(4)]
        string PageBaseZeroKey { get; }
    }
}
