using System;
using System.Linq;

namespace Pagination {
    public class PageSource {
        public IPageConfig Config {
            get => _Config ?? (_Config = new PageConfig());
            set => _Config = value;
        }
        IPageConfig _Config;

        public IPageRequest Request {
            get => _Request ?? (_Request = new PageRequest());
            set => _Request = value;
        }
        IPageRequest _Request;

        public IPage<TItem, TQuery> FindPage<TItem, TQuery>(IOrderedQueryable<TItem> source, TQuery query) {
            var def = default(int);
            var req = Request;
            var conf = Config;

            def = conf.ItemsPerPageDefault;
            var itemsPerPage = req.ItemsPerPage ?? def;
            if (itemsPerPage < 1) itemsPerPage = def;
            if (itemsPerPage > conf.ItemsPerPageMaximum) itemsPerPage = def;

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            def = 0;
            var pageBaseZero = req.PageBaseZero ?? def;
            if (pageBaseZero < 0) pageBaseZero = def;

            var items = source
                .Skip(itemsPerPage * pageBaseZero)
                .Take(itemsPerPage);

            return new Page.Of<TItem>.From<TQuery> {
                Config = conf,
                Items = items,
                ItemsPerPage = itemsPerPage,
                ItemsTotal = totalItems,
                PageBaseZero = pageBaseZero,
                PageTotal = totalPages,
                Query = query,
                Request = req
            };
        }

        public IPage<TItem> FindPage<TItem>(IOrderedQueryable<TItem> source) {
            return FindPage(source, default(object));
        }
    }
}
