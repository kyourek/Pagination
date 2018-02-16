namespace Pagination {
    public interface IPageLink {
        bool IsPageRange { get; }
        bool IsRequestedPage { get; }
        int LinkPageBaseZero { get; }
        string LinkText { get; }
        int LowerPageBaseZero { get; }
        IPage Page { get; }
        int UpperPageBaseZero { get; }
    }
}