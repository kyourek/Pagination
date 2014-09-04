using System;
using System.Linq;
using System.Collections.Generic;

namespace Pagination.Models {

    internal class PageRangeModel : PageLinkModelBase {

        public PageRangeModel(IPageRequestModel request, int lowerPage, int upperPage) : base(request, "...", lowerPage, upperPage) { }

        public override string ToString() {
            return "Page " + LowerPage + " to " + UpperPage;
        }
    }
}
