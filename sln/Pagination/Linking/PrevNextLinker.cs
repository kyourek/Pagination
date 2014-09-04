using System;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;

namespace Pagination.Linking {
   
    internal class PrevNextLinker : PageLinker {

        public PageLinker BaseLinker { get { return _BaseLinker; } }
        private readonly PageLinker _BaseLinker;

        public string PrevText { get { return _PrevText; } }
        private readonly string _PrevText;

        public string NextText { get { return _NextText; } }
        private readonly string _NextText;

        public bool ForcePrevNext { get { return _ForcePrevNext; } }
        private readonly bool _ForcePrevNext;

        public PrevNextLinker(PageLinker baseLinker, string prevText, string nextText, bool forcePrevNext) {
            _PrevText = prevText;
            _NextText = nextText;
            _BaseLinker = baseLinker;
            _ForcePrevNext = forcePrevNext;
        }

        public override IEnumerable<IPageLinkModel> LinkPages(IPageRequestModel request, IPageResultsModel results) {
            if (null == request) throw new ArgumentNullException("request");
            if (null == results) throw new ArgumentNullException("results");

            var list = new List<IPageLinkModel>();
            var forcePrevNext = ForcePrevNext;
            var requestedPage = request.RequestedPage;

            var testPrev = requestedPage > 0;
            if (forcePrevNext || testPrev) {
                list.Add(new PageLinkModel(request, testPrev ? requestedPage - 1 : requestedPage, PrevText));
            }

            var baseLinker = BaseLinker;
            if (baseLinker != null) {
                list.AddRange(baseLinker.LinkPages(request, results));
            }

            var testNext = requestedPage < results.TotalPageCount - 1;
            if (forcePrevNext || testNext) {
                list.Add(new PageLinkModel(request, testNext ? requestedPage + 1 : requestedPage, NextText));
            }

            return list;
        }
    }
}
