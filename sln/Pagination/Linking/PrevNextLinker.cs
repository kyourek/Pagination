﻿using System;
using System.Collections.Generic;

namespace Pagination.Linking {
    class PrevNextLinker : PageLinker {
        public PageLinker BaseLinker { get; set; }
        public string PrevText { get; set; }
        public string NextText { get; set; }
        public bool ForcePrevNext { get; set; }

        public override IEnumerable<PageLink> LinkPages(IPage page) {
            if (null == page) throw new ArgumentNullException(nameof(page));

            var forcePrevNext = ForcePrevNext;
            var requestedPage = page.PageRequested;

            var testPrev = requestedPage > 0;
            if (forcePrevNext || testPrev) {
                yield return new PageLink(
                    page: page, 
                    linkPage: testPrev ? requestedPage - 1 : requestedPage, 
                    linkText: PrevText);
            }

            var baseLinker = BaseLinker;
            if (baseLinker != null) {
                foreach (var pageLink in BaseLinker.LinkPages(page)) {
                    yield return pageLink;
                }
            }

            var testNext = requestedPage < page.PageTotal - 1;
            if (forcePrevNext || testNext) {
                yield return new PageLink(
                    page: page,
                    linkPage: testNext ? requestedPage + 1 : requestedPage, 
                    linkText: NextText);
            }
        }
    }
}
