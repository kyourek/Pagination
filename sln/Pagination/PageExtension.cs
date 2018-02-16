using System;
using System.Collections.Generic;

namespace Pagination {
    public static class PageExtension {
        public static IEnumerable<IPageLink> Links(this IPage page, PageLinker linker) {
            if (null == linker) throw new ArgumentNullException(nameof(linker));
            return linker.Links(page);
        }

        public static PageChain Chain(this IPage page, PageLinker linker) {
            if (null == linker) throw new ArgumentNullException(nameof(linker));
            return linker.Chain(page);
        }
    }
}
