using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("225ACB5A-0C9B-456C-8DFC-E0A4436C9F5C")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPageRequest {
        [ComVisible(false)]
        int? ItemsPerPage { get; }

        [ComVisible(false)]
        int? PageBaseZero { get; }
    }
}
