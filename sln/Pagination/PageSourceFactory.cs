using System;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;
using Pagination.Abstractions;

namespace Pagination {

    /// <summary>
    /// Factory that provides methods for creating ordered and unordered
    /// page data source objects.
    /// </summary>
    public class PageSourceFactory {

        /// <summary>
        /// Gets or sets the maximum number of items that can be
        /// shown on a single page of data source results.
        /// </summary>
        public virtual int MaxItemsPerPage {
            get { return _MaxItemsPerPage; }
            set { _MaxItemsPerPage = value; }
        }
        private int _MaxItemsPerPage = int.MaxValue;


        /// <summary>
        /// Gets or sets the default number of items that are
        /// shown on a page of data source results.
        /// </summary>
        public virtual int DefaultItemsPerPage {
            get { return _DefaultItemsPerPage; }
            set { _DefaultItemsPerPage = value; }
        }
        private int _DefaultItemsPerPage = 50;

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/> and <paramref name="request"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <typeparam name="TRequest">The type of request for which the data source is created.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <param name="request">The request for which the data source is created.</param>
        /// <returns>A new instance of a paged data source.</returns>
        /// <remarks>
        /// If <paramref name="source"/> implements the type <see cref="T:IOrderedQueryable{TSource}"/>,
        /// and instance of <see cref="T:IOrderedPageSourceModel{TSource, TRequest}"/> will be created and returned.
        /// </remarks>
        public virtual IPageSourceModel<TSource, TRequest> CreateSource<TSource, TRequest>(IQueryable<TSource> source, TRequest request) where TRequest : IPageRequestModel {
            var orderedSource = source as IOrderedQueryable<TSource>;
            if (orderedSource != null) {
                return CreateSource<TSource, TRequest>(orderedSource, request);
            }
            return new PageSourceModel<TSource, TRequest>(MaxItemsPerPage, DefaultItemsPerPage, new Source<TSource>(source), request);
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/> and <paramref name="request"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <typeparam name="TRequest">The type of request for which the data source is created.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <param name="request">The request for which the data source is created.</param>
        /// <returns>A new instance of a paged data source.</returns>
        public virtual IOrderedPageSourceModel<TSource, TRequest> CreateSource<TSource, TRequest>(IOrderedQueryable<TSource> source, TRequest request) where TRequest : IPageRequestModel {
            return new OrderedPageSourceModel<TSource, TRequest>(MaxItemsPerPage, DefaultItemsPerPage, new OrderedSource<TSource>(source), request);
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <param name="itemsPerPage">The number of items to be displayed on a page.</param>
        /// <param name="requestedPage">The requested page to be displayed.</param>
        /// <returns>A new instance of a paged data source.</returns>
        /// <remarks>
        /// If <paramref name="source"/> implements the type <see cref="T:IOrderedQueryable{TSource}"/>,
        /// and instance of <see cref="T:IOrderedPageSourceModel"/> will be created and returned.
        /// </remarks>
        public virtual IPageSourceModel<TSource, IPageRequestModel> CreateSource<TSource>(IQueryable<TSource> source, int requestedPage, int itemsPerPage) {
            return CreateSource<TSource, IPageRequestModel>(source, new PageRequestModel { RequestedPage = requestedPage, ItemsPerPage = itemsPerPage });
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <param name="itemsPerPage">The number of items to be displayed on a page.</param>
        /// <param name="requestedPage">The requested page to be displayed.</param>
        /// <returns>A new instance of a paged data source.</returns>
        public virtual IOrderedPageSourceModel<TSource, IPageRequestModel> CreateSource<TSource>(IOrderedQueryable<TSource> source, int requestedPage, int itemsPerPage) {
            return CreateSource<TSource, IPageRequestModel>(source, new PageRequestModel { RequestedPage = requestedPage, ItemsPerPage = itemsPerPage });
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <param name="requestedPage">The requested page to be displayed.</param>
        /// <returns>A new instance of a paged data source.</returns>
        /// <remarks>
        /// If <paramref name="source"/> implements the type <see cref="T:IOrderedQueryable{TSource}"/>,
        /// and instance of <see cref="T:IOrderedPageSourceModel"/> will be created and returned.
        /// </remarks>
        public virtual IPageSourceModel<TSource, IPageRequestModel> CreateSource<TSource>(IQueryable<TSource> source, int requestedPage) {
            return CreateSource(source, requestedPage, DefaultItemsPerPage);
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <param name="requestedPage">The requested page to be displayed.</param>
        /// <returns>A new instance of a paged data source.</returns>
        public virtual IOrderedPageSourceModel<TSource, IPageRequestModel> CreateSource<TSource>(IOrderedQueryable<TSource> source, int requestedPage) {
            return CreateSource(source, requestedPage, DefaultItemsPerPage);
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <returns>A new instance of a paged data source.</returns>
        /// <remarks>
        /// If <paramref name="source"/> implements the type <see cref="T:IOrderedQueryable{TSource}"/>,
        /// and instance of <see cref="T:IOrderedPageSourceModel"/> will be created and returned.
        /// </remarks>
        public virtual IPageSourceModel<TSource, IPageRequestModel> CreateSource<TSource>(IQueryable<TSource> source) {
            return CreateSource(source, 0);
        }

        /// <summary>
        /// Creates a new page data source from the given <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the underlying data source.</typeparam>
        /// <param name="source">The underlying data source of the page source.</param>
        /// <returns>A new instance of a paged data source.</returns>
        public virtual IOrderedPageSourceModel<TSource, IPageRequestModel> CreateSource<TSource>(IOrderedQueryable<TSource> source) {
            return CreateSource(source, 0);
        }
    }
}
