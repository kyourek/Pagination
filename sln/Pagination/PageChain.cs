using System.Collections.Generic;

namespace Pagination {
    public class PageChain {
        public IEnumerable<IPageLink> Links { get; }
        public int PagesTotal { get; }
        public int ItemsTotal { get; }

        public PageChain(int itemsTotal, int pagesTotal, IEnumerable<IPageLink> links) {
            Links = links ?? new PageLink[] { };
            PagesTotal = pagesTotal;
            ItemsTotal = itemsTotal;
        }
    }
}
