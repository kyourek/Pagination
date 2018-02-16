namespace Pagination {
    class PageRequest : IPageRequest {
        public int? ItemsPerPage { get; set; }
        public int? PageBaseZero { get; set; }
    }
}
