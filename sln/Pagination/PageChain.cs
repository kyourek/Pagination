﻿using System.Collections.Generic;

namespace Pagination {
    internal class PageChain : IPageChain {
        public IPage Page { get; }
        public IEnumerable<IPageLink> Links { get; }

        public PageChain(IPage page, IEnumerable<IPageLink> links) {
            Page = page;
            Links = links;
        }
    }
}
