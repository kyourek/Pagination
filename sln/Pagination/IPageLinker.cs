using System.Collections.Generic;

namespace Pagination {
    public interface IPageLinker {
        IEnumerable<IPageLink> Links { get; }
        IPageChain Chain { get; }
        IPageLinker Prev(string text = null, bool force = true);
        IPageLinker Next(string text = null, bool force = true);
        IPageLinker Numbers(bool baseOne = true);
        IPageLinker Dynamic(bool baseOne = true);
    }
}
