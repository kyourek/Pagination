using System.Collections.Generic;

namespace Pagination {
    public class PageChain {
        public IEnumerable<IPageLink> Links { get; }
        public int PageTotal { get; }
        public int ItemsTotal { get; }

        public PageChain(int itemsTotal, int pageTotal, IEnumerable<IPageLink> links) {
            Links = links ?? new PageLink[] { };
            PageTotal = pageTotal;
            ItemsTotal = itemsTotal;
        }
    }
}
