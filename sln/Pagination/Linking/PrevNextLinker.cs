using System;
using System.Collections.Generic;

namespace Pagination.Linking {
    class PrevNextLinker : PageLinker {
        public PageLinker BaseLinker { get; set; }
        public string PrevText { get; set; }
        public string NextText { get; set; }
        public bool ForcePrevNext { get; set; }

        public override IEnumerable<IPageLink> Links(IPage page) {
            if (null == page) throw new ArgumentNullException(nameof(page));

            var forcePrevNext = ForcePrevNext;
            var requestedPage = page.PageBaseZero;

            var testPrev = requestedPage > 0;
            if (forcePrevNext || testPrev) {
                yield return new PageLink(
                    page: page, 
                    linkPageBaseZero: testPrev ? requestedPage - 1 : requestedPage, 
                    linkText: PrevText);
            }

            var baseLinker = BaseLinker;
            if (baseLinker != null) {
                foreach (var pageLink in BaseLinker.Links(page)) {
                    yield return pageLink;
                }
            }

            var testNext = requestedPage < page.PageTotal - 1;
            if (forcePrevNext || testNext) {
                yield return new PageLink(
                    page: page,
                    linkPageBaseZero: testNext ? requestedPage + 1 : requestedPage, 
                    linkText: NextText);
            }
        }
    }
}
