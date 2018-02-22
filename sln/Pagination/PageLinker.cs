using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagination {
    class PageLinker : IPageLinker {
        IEnumerable<IPageLink> PrevLinks(string text, bool force) {
            var pageBaseZero = Page.PageBaseZero;
            var hasPrev = pageBaseZero > 0;
            if (hasPrev || force) {
                yield return new PageLink(
                    page: Page,
                    linkPageBaseZero: hasPrev ? pageBaseZero - 1 : pageBaseZero,
                    linkText: text);
            }
        }

        IEnumerable<IPageLink> NextLinks(string text, bool force) {
            var pageBaseZero = Page.PageBaseZero;
            var hasNext = pageBaseZero < Page.PagesTotal - 1;
            if (hasNext || force) {
                yield return new PageLink(
                    page: Page,
                    linkPageBaseZero: hasNext ? pageBaseZero + 1 : pageBaseZero,
                    linkText: text);
            }
        }

        IEnumerable<IPageLink> NumberedLinks(bool baseOne) {
            return Enumerable
                .Range(0, Page.PagesTotal)
                .Select(pageBaseZero => new PageLink(
                    page: Page,
                    linkPageBaseZero: pageBaseZero,
                    linkText: (pageBaseZero + (baseOne ? 1 : 0)).ToString()));
        }

        public IPage Page { get; }
        public IEnumerable<IPageLink> Links { get; }

        IPageChain _Chain;
        public IPageChain Chain => _Chain ?? (_Chain = new PageChain(Page, Links));

        public PageLinker(IPage page, IEnumerable<IPageLink> links = null) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            Links = links ?? new IPageLink[] { };
        }

        public IPageLinker Prev(string text, bool force) {
            return new PageLinker(Page, Links.Concat(PrevLinks(text ?? "<", force)));
        }

        public IPageLinker Next(string text, bool force) {
            return new PageLinker(Page, Links.Concat(NextLinks(text ?? ">", force)));
        }

        public IPageLinker Numbers(bool baseOne) {
            return new PageLinker(Page, Links.Concat(NumberedLinks(baseOne)));
        }

        public IPageLinker Dynamic(bool baseOne) {
            return new PageLinker(Page, Links.Concat(new DynamicLinker { BaseOne = baseOne }.Links(Page)));
        }

        class DynamicLinker {
            class LinkFactory {
                public IPage Page { get; set; }
                public bool BaseOne { get; set; }

                public PageLink CreateLink(int pageBaseZero, string text) {
                    return new PageLink(
                        page: Page,
                        linkPageBaseZero: pageBaseZero,
                        linkText: text);
                }

                public PageLink CreateLink(int pageBaseZero) {
                    return CreateLink(
                        pageBaseZero: pageBaseZero,
                        text: BaseOne
                            ? (pageBaseZero + 1).ToString()
                            : pageBaseZero.ToString());
                }

                public PageLink CreateRange(int lowerPageBaseZero, int upperPageBaseZero) {
                    return new PageLink(
                        page: Page,
                        linkText: "...",
                        lowerPageBaseZero: lowerPageBaseZero,
                        upperPageBaseZero: upperPageBaseZero);
                }
            }

            public bool BaseOne { get; set; }

            public IEnumerable<IPageLink> Links(IPage page) {
                if (page.PagesTotal > 1) {
                    var requestedPage = page.PageBaseZero;
                    var totalPageCount = page.PagesTotal;
                    var factory = new LinkFactory {
                        BaseOne = BaseOne,
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
}
