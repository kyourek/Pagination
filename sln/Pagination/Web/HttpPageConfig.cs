namespace Pagination.Web {
    public class HttpPageConfig {
        public static HttpPageConfig Default { get; } = new HttpPageConfig();

        public string ItemsPerPageKey {
            get => _ItemsPerPageKey ?? (_ItemsPerPageKey = "_p_ipp");
            set => _ItemsPerPageKey = value;
        }
        string _ItemsPerPageKey;

        public string PageBaseZeroKey {
            get => _PageBaseZeroKey ?? (_PageBaseZeroKey = "_p_pbz");
            set => _PageBaseZeroKey = value;
        }
        string _PageBaseZeroKey;
    }
}
