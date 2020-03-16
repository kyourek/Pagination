using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pagination {
    [Guid("B4F4B89E-5A62-46FB-86EA-B1FF6138A656")]
    [ComVisible(true)]
#if NETCOREAPP
    [ClassInterface(ClassInterfaceType.None)]
#else
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
#endif
    public class PageContext {
        private readonly PageConfig _Config = new PageConfig();

        internal virtual Func<PageRequest> PageRequestFactory {
            get => _PageRequestFactory ?? (_PageRequestFactory = () => new PageRequest());
            set => _PageRequestFactory = value;
        }
        private Func<PageRequest> _PageRequestFactory;

        public IPageConfig Config => _Config;

        public IPageSource<TItem> GetSource<TItem>(IOrderedQueryable<TItem> itemsSource) =>
            new PageSource<TItem> {
                Config = Config,
                ItemsSource = itemsSource,
                Request = PageRequestFactory.Invoke()
            };

        public IPageSource<TItem, TState> GetSource<TItem, TState>(IOrderedQueryable<TItem> itemsSource, TState state) =>
            new PageSource<TItem, TState> {
                Config = Config,
                State = state,
                ItemsSource = itemsSource,
                Request = PageRequestFactory.Invoke()
            };

        public IPage<TItem> ReadPage<TItem>(IOrderedQueryable<TItem> itemsSource) =>
            GetSource(itemsSource).ReadPage();

        public IPage<TItem, TState> ReadPage<TItem, TState>(IOrderedQueryable<TItem> itemsSource, TState state) =>
            GetSource(itemsSource, state).ReadPage();

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
