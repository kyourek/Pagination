using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Pagination.Models {

    internal class PageChainModel : IPageChainModel {

        public IEnumerable<IPageLinkModel> PageLinks { get { return _PageLinks; } }
        private readonly IEnumerable<IPageLinkModel> _PageLinks;

        public int TotalItemCount { get { return _TotalItemCount; } }
        private readonly int _TotalItemCount;

        public int TotalPageCount { get { return _TotalPageCount; } }
        private readonly int _TotalPageCount;

        public PageChainModel(int totalPageCount, int totalItemCount, IEnumerable<IPageLinkModel> pageLinks) {
            if (null == pageLinks) throw new ArgumentNullException("pageLinks");
            _PageLinks = pageLinks;
            _TotalPageCount = totalPageCount;
            _TotalItemCount = totalItemCount;
        }

        public IEnumerator<IPageLinkModel> GetEnumerator() {
            return PageLinks.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return PageLinks.GetEnumerator();
        }
    }
}
