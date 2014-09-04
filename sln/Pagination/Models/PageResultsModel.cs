using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Pagination.Abstractions;

namespace Pagination.Models {

    internal class PageResultsModel<TSource, TRequest> : IPageResultsModel, IPageResultsModel<TSource>, IPageResultsModel<TSource, TRequest> where TRequest : IPageRequestModel {

        public int TotalPageCount { get { return _TotalPageCount; } }
        private readonly int _TotalPageCount;

        public int TotalItemCount { get { return _TotalItemCount; } }
        private readonly int _TotalItemCount;

        public ISource<TSource> Results { get { return _Results; } }
        private readonly ISource<TSource> _Results;

        public IOrderedPageSourceModel<TSource, TRequest> Source { get { return _Source; } }
        private readonly IOrderedPageSourceModel<TSource, TRequest> _Source;

        public PageResultsModel(int totalPageCount, int totalItemCount, IOrderedPageSourceModel<TSource, TRequest> source, ISource<TSource> results) {
            if (null == source) throw new ArgumentNullException("source");
            if (null == results) throw new ArgumentNullException("results");
            _Source = source;
            _Results = results;
            _TotalPageCount = totalPageCount;
            _TotalItemCount = totalItemCount;
        }

        public IPageChainModel LinkPages(PageLinker pageLinker) {
            if (null == pageLinker) throw new ArgumentNullException("pageLinker");
            return new PageChainModel(TotalPageCount, TotalItemCount, pageLinker.LinkPages(Source.Request, this));
        }

        public IPageChainModel LinkPages() {
            return LinkPages(PageLinker.Default);
        }
    }
}
