using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Pagination.Models;

namespace Pagination.Linking {
    
    internal class NumberLinker : PageLinker {

        public bool IsBase1 { get { return _IsBase1; } }
        private readonly bool _IsBase1;

        public NumberLinker(bool isBase1) {
            _IsBase1 = isBase1;
        }

        public override IEnumerable<IPageLinkModel> LinkPages(IPageRequestModel request, IPageResultsModel results) {
            if (null == request) throw new ArgumentNullException("request");
            if (null == results) throw new ArgumentNullException("results");

            var list = new List<IPageLinkModel>();
            var isBase1 = IsBase1;
            var requestedPage = request.RequestedPage;
            for (var i = 0; i < results.TotalPageCount; i++) {
                list.Add(new PageLinkModel(request, i, isBase1 ? (i + 1).ToString() : i.ToString()));
            }

            return list;
        }
    }
}
