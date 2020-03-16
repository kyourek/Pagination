using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("0C9FE26B-13B6-4298-A22D-C92683199DAA")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPageChain {
        [DispId(1)]
        IPage Page { get; }

        [ComVisible(false)]
        IEnumerable<IPageLink> Links { get; }
    }
}
