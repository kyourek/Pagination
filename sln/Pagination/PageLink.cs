using System;

namespace Pagination {
    internal class PageLink : IPageLink {
        public int LinkPageBaseZero { get; }
        public int LowerPageBaseZero { get; }
        public int UpperPageBaseZero { get; }
        public bool IsPageRange { get; }
        public bool IsRequestedPage { get; }
        public string LinkText { get; }
        public IPage Page { get; }
        public int LinkPageBaseOne => LinkPageBaseZero + 1;
        public int LowerPageBaseOne => LowerPageBaseZero + 1;
        public int UpperPageBaseOne => UpperPageBaseZero + 1;

        public PageLink(IPage page, int linkPageBaseZero, string linkText) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            LinkPageBaseZero = linkPageBaseZero;
            LinkText = linkText;
            LowerPageBaseZero = linkPageBaseZero;
            UpperPageBaseZero = linkPageBaseZero;
            IsPageRange = false;
            IsRequestedPage = Page.PageBaseZero == linkPageBaseZero;
        }

        public PageLink(IPage page, string linkText, int lowerPageBaseZero, int upperPageBaseZero) {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            LinkPageBaseZero = lowerPageBaseZero;
            LinkText = linkText;
            LowerPageBaseZero = lowerPageBaseZero;
            UpperPageBaseZero = upperPageBaseZero;
            IsPageRange = true;
            IsRequestedPage = false;
        }
    }
}
