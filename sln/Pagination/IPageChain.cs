using System.Collections.Generic;

namespace Pagination {
    public interface IPageChain {
        IPage Page { get; }
        IEnumerable<IPageLink> Links { get; }
    }
}
