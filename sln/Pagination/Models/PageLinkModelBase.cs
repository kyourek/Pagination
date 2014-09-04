using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Pagination.Models {

    internal abstract class PageLinkModelBase : IPageLinkModel {

        public int LinkPage { get { return _LinkPage; } }
        private readonly int _LinkPage;

        public int LinkPageBase0 {
            get { return LinkPage; }
        }

        public int LinkPageBase1 {
            get { return LinkPage + 1; }
        }

        public int LowerPage { get { return _LowerPage; } }
        private readonly int _LowerPage;

        public int UpperPage { get { return _UpperPage; } }
        private readonly int _UpperPage;

        public bool IsPageRange { get { return _IsPageRange; } }
        private readonly bool _IsPageRange;

        public bool IsRequestedPage { get { return _IsRequestedPage; } }
        private readonly bool _IsRequestedPage;

        public string LinkText { get { return _LinkText; } }
        private readonly string _LinkText;

        public IPageRequestModel Request { get { return _Request; } }
        private readonly IPageRequestModel _Request;

        protected PageLinkModelBase(IPageRequestModel request, int linkPage, string linkText) {
            if (null == request) throw new ArgumentNullException("request");
            _Request = request;
            _LinkPage = linkPage;
            _LinkText = linkText;
            _LowerPage = linkPage;
            _UpperPage = linkPage;
            _IsPageRange = false;
            _IsRequestedPage = request.RequestedPage == linkPage;
        }

        protected PageLinkModelBase(IPageRequestModel request, string linkText, int lowerPage, int upperPage) {
            if (null == request) throw new ArgumentNullException("request");
            _Request = request;
            _LinkPage = lowerPage;
            _LinkText = linkText;
            _LowerPage = lowerPage;
            _UpperPage = upperPage;
            _IsPageRange = true;
            _IsRequestedPage = false;
        }
    }
}
