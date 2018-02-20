using System;
using System.Linq;
using System.Collections.Generic;

namespace Pagination.Linking {
    class NumberLinker : PageLinker {
        public bool BaseOne { get; set; }

        public override IEnumerable<IPageLink> Links(IPage page) {
            if (null == page) throw new ArgumentNullException(nameof(page));

            var baseOne = BaseOne;
            var requestedPage = page.PageBaseZero;
            var totalPageCount = page.PagesTotal;
            return Enumerable
                .Range(0, page.PagesTotal)
                .Select(i => new PageLink(
                    page: page,
                    linkPageBaseZero: i,
                    linkText: baseOne ? (i + 1).ToString() : i.ToString()));
        }
    }
}
