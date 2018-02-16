namespace Pagination {
    public interface IPageRequest {
        int? ItemsPerPage { get; }
        int? PageBaseZero { get; }
    }
}