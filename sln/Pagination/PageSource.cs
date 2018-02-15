using System;
using System.Linq;

namespace Pagination {
    public class PageSource {
        PageRequest _Request;
        public PageRequest Request {
            get { return _Request ?? (_Request = new PageRequest()); }
            set { _Request = value; }
        }

        public int ItemsPerPageDefault { get; set; } = 25;
        public int ItemsPerPageMaximum { get; set; } = 100;

        public IPage<TItem, TQuery> FindPage<TItem, TQuery>(IOrderedQueryable<TItem> source, TQuery query) {
            var req = Request;
            var def = default(int);

            def = ItemsPerPageDefault;
            var itemsPerPage = req.ItemsPerPage ?? def;
            if (itemsPerPage < 1) itemsPerPage = def;
            if (itemsPerPage > ItemsPerPageMaximum) itemsPerPage = def;

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            def = 0;
            var pageBaseZero = req.PageBaseZero ?? def;
            if (pageBaseZero < 1) pageBaseZero = def;

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
