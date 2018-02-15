using System.Collections.Generic;

namespace Pagination {
    class PageChain {
        public IEnumerable<PageLink> Links { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }

        public PageChain(int totalPages, int totalItems, IEnumerable<PageLink> links) {
            Links = links ?? new PageLink[] { };
            TotalPages = totalPages;
            TotalItems = totalItems;
        }
    }
}
