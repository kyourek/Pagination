using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagination {
    using Web;

    public class PageFactory {
        int? GetContextValue(string key) {
            if (ContextValues.TryGetValue(key, out object value)) {
                if (int.TryParse(value?.ToString(), out int i)) {
                    return i;
                }
            }
            return null;
        }

        IDictionary<string, object> _ContextValues;
        IDictionary<string, object> ContextValues {
            get { return _ContextValues ?? (_ContextValues = Context.GetValues(new[] { PageRequestedKey, ItemsRequestedKey })); }
        }

        IPageContext _Context;
        public IPageContext Context {
            get { return _Context ?? (_Context = new HttpPageContext()); }
            set {
                if (_Context != value) {
                    _Context = value;
                    _ContextValues = null;
                }
            }
        }

        string _PageRequestedKey;
        public string PageRequestedKey {
            get { return _PageRequestedKey ?? (_PageRequestedKey = "_PageRequested"); }
            set {
                if (_PageRequestedKey != value) {
                    _PageRequestedKey = value;
                    _ContextValues = null;
                }
            }
        }

        string _ItemsRequestedKey;
        public string ItemsRequestedKey {
            get { return _ItemsRequestedKey ?? (_ItemsRequestedKey = "_ItemsRequested"); }
            set {
                if (_ItemsRequestedKey != value) {
                    _ItemsRequestedKey = value;
                    _ContextValues = null;
                }
            }
        }

        public int DefaultItemsRequested { get; set; } = 25;
        public int MaximumItemsRequested { get; set; } = 100;

        public IPage<TItem, TQuery> CreatePage<TItem, TQuery>(IOrderedQueryable<TItem> source, TQuery query) {
            var ctx = Context;
            var def = default(int);

            def = DefaultItemsRequested;
            var itemsRequested = GetContextValue(ItemsRequestedKey) ?? def;
            if (itemsRequested < 1) itemsRequested = def;
            if (itemsRequested > MaximumItemsRequested) itemsRequested = def;

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsRequested);

            def = 0;
            var pageRequested = GetContextValue(PageRequestedKey) ?? def;
            if (pageRequested < 1) pageRequested = def;

            var items = source
                .Skip(itemsRequested * pageRequested)
                .Take(itemsRequested);

            return new Page<TItem, TQuery> {
                Items = items,
                ItemsRequested = itemsRequested,
                ItemsRequestedKey = ItemsRequestedKey,
                ItemsTotal = totalItems,
                PageRequested = pageRequested,
                PageRequestedKey = PageRequestedKey,
                PageTotal = totalPages,
                Query = query
            };
        }

        public IPage<TItem> CreatePage<TItem>(IOrderedQueryable<TItem> source) {
            return CreatePage(source, default(object));
        }
    }
}
