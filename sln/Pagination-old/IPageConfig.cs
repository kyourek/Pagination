namespace Pagination {
    public interface IPageConfig {
        int ItemsPerPageDefault { get; }
        string ItemsPerPageKey { get; }
        int ItemsPerPageMaximum { get; }
        string PageBaseZeroKey { get; }
    }
}