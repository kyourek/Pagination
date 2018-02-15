using System;
using System.Linq;
using System.Collections.Generic;

namespace Pagination.Linking {
    class NumberLinker : PageLinker {
        public bool IsBase1 { get; set; }

        public override IEnumerable<PageLink> LinkPages(IPage page) {
            if (null == page) throw new ArgumentNullException(nameof(page));

            var isBase1 = IsBase1;
            var requestedPage = page.PageRequested;
            var totalPageCount = page.PageTotal;
            return Enumerable
                .Range(0, page.PageTotal)
                .Select(i => new PageLink(
                    page: page,
                    linkPage: i,
                    linkText: isBase1 ? (i + 1).ToString() : i.ToString()));
        }
    }
}
