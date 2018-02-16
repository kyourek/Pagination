using System;
using System.Collections.Generic;

namespace Pagination {
    using Linking;

    abstract class PageLinker {
        public static PageLinker Default {
            get {
                if (null == _Default) _Default = Create();
                return _Default;
            }
            set {
                if (null == value) throw new ArgumentNullException("Default");
                _Default = value;
            }
        }
        private static PageLinker _Default;

        public abstract IEnumerable<PageLink> LinkPages(IPage page);

        public static PageLinker Create(bool isBase1, string prevText, string nextText, bool forcePrevNext) {
            var baseLinker = new NumberLinker { BaseOne = isBase1 };
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

        public static PageLinker Create(bool isBase1, string prevText, string nextText) {
            return Create(isBase1, prevText, nextText, true);
        }

        public static PageLinker Create(string prevText, string nextText) {
            return Create(true, prevText, nextText);
        }

        public static PageLinker Create(bool isBase1) {
            return Create(isBase1, null, null, false);
        }

        public static PageLinker Create() {
            return Create(true);
        }

        public static PageLinker CreateDynamic(bool isBase1, string prevText, string nextText, bool forcePrevNext) {
            var baseLinker = new DynamicLinker { BaseOne = isBase1 };
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

        public static PageLinker CreateDynamic(bool isBase1, string prevText, string nextText) {
            return CreateDynamic(isBase1, prevText, nextText, true);
        }

        public static PageLinker CreateDynamic(string prevText, string nextText) {
            return CreateDynamic(true, prevText, nextText);
        }

        public static PageLinker CreateDynamic(bool isBase1) {
            return CreateDynamic(isBase1, null, null, false);
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
