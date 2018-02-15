namespace Pagination {
    public interface IPageLink {
        bool IsPageRange { get; }
        bool IsRequestedPage { get; }
        int LinkPage { get; }
        int LinkPageBase0 { get; }
        int LinkPageBase1 { get; }
        string LinkText { get; }
        int LowerPage { get; }
        IPage Page { get; }
        int UpperPage { get; }
    }
}