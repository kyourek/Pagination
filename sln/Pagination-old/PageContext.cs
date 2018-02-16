namespace Pagination {
    public class PageContext {
        protected virtual IPageRequest GetRequest() {
            return new PageRequest();
        }

        public IPageConfig Config => _Config;
        readonly PageConfig _Config = new PageConfig();

        public PageSource GetSource() {
            return new PageSource {
                Config = Config,
                Request = GetRequest()
            };
        }

        public PageContext SetItemsPerPageDefault(int value) {
            _Config.ItemsPerPageDefault = value;
            return this;
        }

        public PageContext SetItemsPerPageMaximum(int value) {
            _Config.ItemsPerPageMaximum = value;
            return this;
        }

        public PageContext SetItemsPerPageKey(string value) {
            _Config.ItemsPerPageKey = value;
            return this;
        }

        public PageContext SetPageBaseZeroKey(string value) {
            _Config.PageBaseZeroKey = value;
            return this;
        }
    }
}
