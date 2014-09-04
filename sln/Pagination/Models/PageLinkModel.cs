using System;
using System.Linq;
using System.Collections.Generic;

namespace Pagination.Models {

    internal class PageLinkModel : PageLinkModelBase {

        public PageLinkModel(IPageRequestModel request, int linkPage, string linkText) : base(request, linkPage, linkText) { }

        public override string ToString() {
            var linkText = LinkText;
            var linkPage = LinkPage.ToString();
            return "Page " + linkPage + (linkPage == linkText ? "" : " (" + linkText + ")") + (IsRequestedPage ? " Is Requested" : "");
        }
    }
}
