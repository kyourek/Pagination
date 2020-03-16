using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("66036483-3D29-4A0B-97A3-7A830D9A4755")]
    [ComVisible(true)]
#if NETCOREAPP
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
#else
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
#endif
    public interface IPageLinker {
        [DispId(1)]
        IPageChain Chain { get; }

        [DispId(2)]
        IPageLinker Prev(string text = null, bool force = true);

        [DispId(3)]
        IPageLinker Next(string text = null, bool force = true);

        [DispId(4)]
        IPageLinker Numbers(bool baseOne = true);

        [DispId(5)]
        IPageLinker Dynamic(bool baseOne = true);

        [ComVisible(false)]
        IEnumerable<IPageLink> Links { get; }
    }
}
