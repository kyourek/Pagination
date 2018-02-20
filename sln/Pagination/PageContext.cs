using System.Linq;

namespace Pagination {
    public class PageContext {
        internal PageRequest DefaultRequest {
            get => _DefaultRequest ?? (_DefaultRequest = new PageRequest());
            set => _DefaultRequest = value;
        }
        PageRequest _DefaultRequest;

        protected virtual IPageRequest GetRequest() {
            return DefaultRequest;
        }

        public IPageConfig Config => _Config;
        readonly PageConfig _Config = new PageConfig();

        public IPageSource<TItem> GetSource<TItem>(IOrderedQueryable<TItem> query) {
            return new PageSource<TItem> {
                Config = Config,
                ItemsSource = query,
                Request = GetRequest()
            };
        }

        public IPageSource<TItem, TFilter> GetSource<TItem, TFilter>(IOrderedQueryable<TItem> query, TFilter filter) {
            return new PageSource<TItem, TFilter> {
                Config = Config,
                Filter = filter,
                ItemsSource = query,
                Request = GetRequest()
            };
        }

        public IPage<TItem> FindPage<TItem>(IOrderedQueryable<TItem> query) {
            return GetSource(query).FindPage();
        }

        public IPage<TItem, TFilter> FindPage<TItem, TFilter>(IOrderedQueryable<TItem> query, TFilter filter) {
            return GetSource(query, filter).FindPage();
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
