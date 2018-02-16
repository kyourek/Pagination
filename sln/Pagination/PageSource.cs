using System;
using System.Linq;

namespace Pagination {
    public class PageSource {
        public int? PageBaseZero { get; set; }
        public int? ItemsPerPage { get; set; }
        public int ItemsPerPageDefault { get; set; } = 25;
        public int ItemsPerPageMaximum { get; set; } = 100;

        public IPage<TItem, TQuery> FindPage<TItem, TQuery>(IOrderedQueryable<TItem> source, TQuery query) {
            var def = default(int);

            def = ItemsPerPageDefault;
            var itemsPerPage = ItemsPerPage ?? def;
            if (itemsPerPage < 1) itemsPerPage = def;
            if (itemsPerPage > ItemsPerPageMaximum) itemsPerPage = def;

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            def = 0;
            var pageBaseZero = PageBaseZero ?? def;
            if (pageBaseZero < 0) pageBaseZero = def;

            var items = source
                .Skip(itemsPerPage * pageBaseZero)
                .Take(itemsPerPage);

            return new Page<TItem, TQuery> {
                Items = items,
                ItemsPerPage = itemsPerPage,
                ItemsTotal = totalItems,
                PageBaseZero = pageBaseZero,
                PageTotal = totalPages,
                Query = query
            };
        }

        public IPage<TItem> FindPage<TItem>(IOrderedQueryable<TItem> source) {
            return FindPage(source, default(object));
        }
    }
}
