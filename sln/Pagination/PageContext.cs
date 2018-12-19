using System;
using System.Linq;

namespace Pagination {
    public class PageContext {
        Func<PageRequest> _GetRequest;
        internal virtual Func<PageRequest> GetRequest {
            get => _GetRequest ?? (_GetRequest = () => new PageRequest());
            set => _GetRequest = value;
        }

        public IPageConfig Config => _Config;
        readonly PageConfig _Config = new PageConfig();

        public IPageSource<TItem> GetSource<TItem>(IOrderedQueryable<TItem> itemsSource) {
            return new PageSource<TItem> {
                Config = Config,
                ItemsSource = itemsSource,
                Request = GetRequest()
            };
        }

        public IPageSource<TItem, TState> GetSource<TItem, TState>(IOrderedQueryable<TItem> itemsSource, TState state) {
            return new PageSource<TItem, TState> {
                Config = Config,
                State = state,
                ItemsSource = itemsSource,
                Request = GetRequest()
            };
        }

        [Obsolete("Use 'ReadPage' instead.")]
        public IPage<TItem> FindPage<TItem>(IOrderedQueryable<TItem> itemsSource) {
            return ReadPage(itemsSource);
        }

        public IPage<TItem> ReadPage<TItem>(IOrderedQueryable<TItem> itemsSource) {
            return GetSource(itemsSource).ReadPage();
        }

        [Obsolete("Use 'ReadPage' instead.")]
        public IPage<TItem, TState> FindPage<TItem, TState>(IOrderedQueryable<TItem> itemsSource, TState state) {
            return ReadPage(itemsSource, state);
        }

        public IPage<TItem, TState> ReadPage<TItem, TState>(IOrderedQueryable<TItem> itemsSource, TState state) {
            return GetSource(itemsSource, state).ReadPage();
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
