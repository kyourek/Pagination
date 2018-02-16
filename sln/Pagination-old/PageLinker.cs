using System.Collections.Generic;

namespace Pagination {
    using Linking;

    public abstract class PageLinker {
        public abstract IEnumerable<IPageLink> Links(IPage page);

        public PageChain Chain(IPage page) {
            return new PageChain(
                itemsTotal: page.ItemsTotal, 
                pageTotal: page.PageTotal, 
                links: Links(page));
        }

        public static PageLinker Create(bool baseOne, string prevText, string nextText, bool forcePrevNext) {
            var baseLinker = new NumberLinker { BaseOne = baseOne };
            if (string.IsNullOrEmpty(prevText) && string.IsNullOrEmpty(nextText)) {
                return baseLinker;
            }
            return new PrevNextLinker {
                BaseLinker = baseLinker,
                PrevText = prevText,
                NextText = nextText,
                ForcePrevNext = forcePrevNext
            };
        }

        public static PageLinker Create(bool baseOne, string prevText, string nextText) {
            return Create(baseOne, prevText, nextText, true);
        }

        public static PageLinker Create(string prevText, string nextText) {
            return Create(true, prevText, nextText);
        }

        public static PageLinker Create(bool baseOne) {
            return Create(baseOne, null, null, false);
        }

        public static PageLinker Create() {
            return Create(true);
        }

        public static PageLinker CreateDynamic(bool baseOne, string prevText, string nextText, bool forcePrevNext) {
            var baseLinker = new DynamicLinker { BaseOne = baseOne };
            if (string.IsNullOrEmpty(prevText) && string.IsNullOrEmpty(nextText)) {
                return baseLinker;
            }
            return new PrevNextLinker {
                BaseLinker = baseLinker,
                PrevText = prevText,
                NextText = nextText,
                ForcePrevNext = forcePrevNext
            };
        }

        public static PageLinker CreateDynamic(bool baseOne, string prevText, string nextText) {
            return CreateDynamic(baseOne, prevText, nextText, true);
        }

        public static PageLinker CreateDynamic(string prevText, string nextText) {
            return CreateDynamic(true, prevText, nextText);
        }

        public static PageLinker CreateDynamic(bool baseOne) {
            return CreateDynamic(baseOne, null, null, false);
        }

        public static PageLinker CreateDynamic() {
            return CreateDynamic(true);
        }

        public static PageLinker CreatePrevNext(string prevText, string nextText, bool forcePrevNext) {
            return new PrevNextLinker {
                PrevText = prevText,
                NextText = nextText,
                ForcePrevNext = forcePrevNext
            };
        }

        public static PageLinker CreatePrevNext(string prevText, string nextText) {
            return CreatePrevNext(prevText, nextText, true);
        }
    }
}
