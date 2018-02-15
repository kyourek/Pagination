using System.Collections.Generic;

namespace Pagination.Linking {
    class DynamicLinker : PageLinker {
        class LinkFactory {
            public IPage Page { get; set; }
            public bool IsBase1 { get; set; }

            public PageLink CreateLink(int page, string text) {
                return new PageLink(
                    page: Page, 
                    linkPage: page, 
                    linkText: text);
            }

            public PageLink CreateLink(int page) {
                return CreateLink(
                    page: page, 
                    text: IsBase1 ? (page + 1).ToString() : page.ToString());
            }

            public PageLink CreateRange(int lower, int upper) {
                return new PageLink(
                    page: Page,
                    linkText: "...",
                    lowerPage: lower,
                    upperPage: upper);
            }
        }

        public bool IsBase1 { get; set; }

        public override IEnumerable<PageLink> LinkPages(IPage page) {
            if (page.PageTotal > 1) {
                var requestedPage = page.PageBaseZero;
                var totalPageCount = page.PageTotal;
                var factory = new LinkFactory {
                    IsBase1 = IsBase1,
                    Page = page
                };

                yield return factory.CreateLink(0);

                if (requestedPage > 1) {
                    var test = requestedPage == totalPageCount - 1 && totalPageCount > 3;
                    yield return factory.CreateRange(1, test ? requestedPage - 3 : requestedPage - 2);
                    if (test) {
                        yield return factory.CreateLink(requestedPage - 2);
                    }
                    yield return factory.CreateLink(requestedPage - 1);
                }

                if (requestedPage != 0 && requestedPage != totalPageCount - 1) {
                    yield return factory.CreateLink(requestedPage);
                }

                if (requestedPage < totalPageCount - 2) {
                    var test = requestedPage == 0 && totalPageCount > 3;
                    yield return factory.CreateLink(requestedPage + 1);
                    if (test) {
                        yield return factory.CreateLink(requestedPage + 2);
                    }
                    yield return factory.CreateRange(test ? requestedPage + 3 : requestedPage + 2, totalPageCount - 2);
                }

                yield return factory.CreateLink(totalPageCount - 1);
            }
        }
    }
}
