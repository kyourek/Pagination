using System;

namespace Pagination {
    class PageLink : IPageLink {
        public int LinkPage { get; }
        public int LinkPageBase0 => LinkPage;
        public int LinkPageBase1 => LinkPage + 1;

        public int LowerPage { get; }
        public int UpperPage { get; }
        public bool IsPageRange { get; }
        public bool IsRequestedPage { get; }

        public string LinkText { get; }

        public IPage Page { get; }

        public PageLink(IPage page, int linkPage, string linkText) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            LinkPage = linkPage;
            LinkText = linkText;
            LowerPage = linkPage;
            UpperPage = linkPage;
            IsPageRange = false;
            IsRequestedPage = Page.PageBaseZero == linkPage;
        }

        public PageLink(IPage page, string linkText, int lowerPage, int upperPage) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            LinkPage = lowerPage;
            LinkText = linkText;
            LowerPage = lowerPage;
            UpperPage = upperPage;
            IsPageRange = true;
            IsRequestedPage = false;
        }
    }
}
