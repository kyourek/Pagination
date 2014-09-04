using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Pagination.Abstractions;

namespace Pagination.Models {

    internal class PageSourceModel<TSource, TRequest> : IPageSourceModel, IPageSourceModel<TSource, TRequest> where TRequest : IPageRequestModel {

        private readonly int _MaxItemsPerPage;
        private readonly int _DefaultItemsPerPage;

        public TRequest Request { get { return _Request; } }
        private readonly TRequest _Request;

        public ISource<TSource> Source { get { return _Source; } }
        private readonly ISource<TSource> _Source;

        public PageSourceModel(int maxItemsPerPage, int defaultItemsPerPage, ISource<TSource> source, TRequest request) {
            if (null == source) throw new ArgumentNullException("source");
            if (null == request) throw new ArgumentNullException("request");
            _Source = source;
            _Request = request;
            _MaxItemsPerPage = maxItemsPerPage;
            _DefaultItemsPerPage = defaultItemsPerPage;
        }

        public int GetMaxItemsPerPage() {
            return _MaxItemsPerPage;
        }

        public int GetDefaultItemsPerPage() {
            return _DefaultItemsPerPage;
        }

        public virtual IPageSourceModel<TSource, TRequest> MaxItemsPerPage(int value) {
            return new PageSourceModel<TSource, TRequest>(value, GetDefaultItemsPerPage(), Source, Request);
        }

        public virtual IPageSourceModel<TSource, TRequest> DefaultItemsPerPage(int value) {
            return new PageSourceModel<TSource, TRequest>(GetMaxItemsPerPage(), value, Source, Request);
        }

        public IOrderedPageSourceModel<TSource, TRequest> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector) {
            return new OrderedPageSourceModel<TSource, TRequest>(GetMaxItemsPerPage(), GetDefaultItemsPerPage(), Source.OrderBy(keySelector), Request);
        }

        public IOrderedPageSourceModel<TSource, TRequest> OrderByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector) {
            return new OrderedPageSourceModel<TSource, TRequest>(GetMaxItemsPerPage(), GetDefaultItemsPerPage(), Source.OrderByDescending(keySelector), Request);
        }
    }
}
