using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Pagination.Abstractions;

namespace Pagination.Models {

    internal class OrderedPageSourceModel<TSource, TRequest> : PageSourceModel<TSource, TRequest>, IOrderedPageSourceModel<TSource, TRequest> where TRequest : IPageRequestModel {

        private IOrderedSource<TSource> OrderedSource { get { return (IOrderedSource<TSource>)Source; } }

        public OrderedPageSourceModel(int maxItemsPerPage, int defaultItemsPerPage, IOrderedSource<TSource> orderedSource, TRequest request) : base(maxItemsPerPage, defaultItemsPerPage, orderedSource, request) { }

        public IOrderedPageSourceModel<TSource, TRequest> ThenBy<TKey>(Expression<Func<TSource, TKey>> keySelector) {
            return new OrderedPageSourceModel<TSource, TRequest>(GetMaxItemsPerPage(), GetDefaultItemsPerPage(), OrderedSource.ThenBy(keySelector), Request);
        }

        public IOrderedPageSourceModel<TSource, TRequest> ThenByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector) {
            return new OrderedPageSourceModel<TSource, TRequest>(GetMaxItemsPerPage(), GetDefaultItemsPerPage(), OrderedSource.ThenByDescending(keySelector), Request);
        }

        public IPageResultsModel<TSource, TRequest> Query() {

            var request = Request;
            var itemsPerPage = request.ItemsPerPage;
            var maxItemsPerPage = GetMaxItemsPerPage();
            if (itemsPerPage > maxItemsPerPage) {
                itemsPerPage = maxItemsPerPage;
            }
            if (itemsPerPage < 1) {
                itemsPerPage = GetDefaultItemsPerPage();
            }

            var orderedSource = OrderedSource;
            var totalItemCount = orderedSource.Count();
            var totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)itemsPerPage);

            var requestedPage = request.RequestedPage;
            if (requestedPage < 1) {
                requestedPage = 0;
            }

            var results = new Source<TSource>(orderedSource.Skip(itemsPerPage * requestedPage).Take(itemsPerPage));

            return new PageResultsModel<TSource, TRequest>(totalPageCount, totalItemCount, this, results);
        }

        public override IPageSourceModel<TSource, TRequest> MaxItemsPerPage(int value) {
            return new OrderedPageSourceModel<TSource, TRequest>(value, GetDefaultItemsPerPage(), OrderedSource, Request);
        }

        public override IPageSourceModel<TSource, TRequest> DefaultItemsPerPage(int value) {
            return new OrderedPageSourceModel<TSource, TRequest>(GetMaxItemsPerPage(), value, OrderedSource, Request);
        }

        IOrderedSource<TSource> IOrderedPageSourceModel<TSource>.Source {
            get { return OrderedSource; }
        }

        IOrderedPageSourceModel<TSource, TRequest> IOrderedPageSourceModel<TSource, TRequest>.MaxItemsPerPage(int value) {
            return (IOrderedPageSourceModel<TSource, TRequest>)MaxItemsPerPage(value);
        }

        IOrderedPageSourceModel<TSource, TRequest> IOrderedPageSourceModel<TSource, TRequest>.DefaultItemsPerPage(int value) {
            return (IOrderedPageSourceModel<TSource, TRequest>)DefaultItemsPerPage(value);
        }
    }
}
