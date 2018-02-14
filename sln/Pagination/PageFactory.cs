using System;
using System.Linq;

namespace Pagination {
    public class PageFactory {
        PageContext _PageContext;
        public PageContext PageContext {
            get { return _PageContext ?? (_PageContext = new PageContext()); }
            set { _PageContext = value; }
        }

        public int DefaultItemsRequested { get; set; }
        public int MaximumItemsRequested { get; set; }

        public Page<TItem> CreatePage<TItem>(IOrderedQueryable<TItem> source) {
            var ctx = PageContext;
            var def = default(int);

            def = DefaultItemsRequested;
            var itemsRequested = ctx.GetItemsRequested() ?? def;
            if (itemsRequested < 1) itemsRequested = def;
            if (itemsRequested > MaximumItemsRequested) itemsRequested = def;

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsRequested);

            def = 0;
            var pageRequested = ctx.GetPageRequested() ?? def;
            if (pageRequested < 1) pageRequested = def;

            var items = source
                .Skip(itemsRequested * pageRequested)
                .Take(itemsRequested);

            return new Page<TItem>(
                pageRequested: pageRequested,
                pageTotal: totalPages,
                itemsRequested: itemsRequested,
                itemsTotal: totalItems,
                items: items,
                query: null);
        }

        public Page<TItem, TQuery> CreatePage<TItem, TQuery>(IOrderedQueryable<TItem> source, TQuery query) {
            var page = CreatePage(source);
            return new Page<TItem, TQuery>(
                pageRequested: page.PageRequested,
                pageTotal: page.PageTotal,
                itemsRequested: page.ItemsRequested,
                itemsTotal: page.ItemsTotal,
                items: page.Items,
                query: query);
        }
    }
}
