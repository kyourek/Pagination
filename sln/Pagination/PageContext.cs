﻿using System.Linq;

namespace Pagination {
    public class PageContext {
        internal PageRequest DefaultRequest {
            get => _DefaultRequest ?? (_DefaultRequest = new PageRequest());
            set => _DefaultRequest = value;
        }
        PageRequest _DefaultRequest;

        internal virtual PageRequest GetRequest() {
            return new PageRequest {
                ItemsPerPage = DefaultRequest.ItemsPerPage,
                PageBaseZero = DefaultRequest.PageBaseZero
            };
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

        public IPage<TItem> FindPage<TItem>(IOrderedQueryable<TItem> itemsSource) {
            return GetSource(itemsSource).FindPage();
        }

        public IPage<TItem, TState> FindPage<TItem, TState>(IOrderedQueryable<TItem> itemsSource, TState state) {
            return GetSource(itemsSource, state).FindPage();
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
