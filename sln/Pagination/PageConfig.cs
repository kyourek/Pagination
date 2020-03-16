namespace Pagination {
    internal class PageConfig : IPageConfig {
        public int ItemsPerPageDefault { get; set; } = 25;
        public int ItemsPerPageMaximum { get; set; } = 100;

        public string ItemsPerPageKey {
            get => _ItemsPerPageKey ?? (_ItemsPerPageKey = "_p_ipp");
            set => _ItemsPerPageKey = value;
        }
        private string _ItemsPerPageKey;

        public string PageBaseZeroKey {
            get => _PageBaseZeroKey ?? (_PageBaseZeroKey = "_p_pbz");
            set => _PageBaseZeroKey = value;
        }
        private string _PageBaseZeroKey;
    }
}
