using System;
using System.Linq;
using System.Collections.Generic;

using Pagination.Abstractions;

namespace Pagination.Models {
    
    /// <summary>
    /// Interface for the results of a pagination request.
    /// </summary>
    public interface IPageResultsModel {

        /// <summary>
        /// The total number of pages available for the request.
        /// </summary>
        int TotalPageCount { get; }

        /// <summary>
        /// The total number of items available for the request.
        /// </summary>
        int TotalItemCount { get; }

        /// <summary>
        /// Creates a collection of <see cref="T:IPageLinkModel"/> that can be used to display pagination.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:IPageChainModel"/> that contains the collection of <see cref="T:IPageLinkModel"/> that can be used to display pagination.
        /// </returns>
        IPageChainModel LinkPages();

        /// <summary>
        /// Creates a collection of <see cref="T:IPageLinkModel"/> using the specified <paramref name="pageLinker"/> that can be used to display pagination.
        /// </summary>
        /// <param name="pageLinker">The <see cref="T:PageLinker"/> that creates the <see cref="T:IPageChainModel"/>.</param>
        /// <returns>
        /// A new <see cref="T:IPageChainModel"/> that contains the collection of <see cref="T:IPageLinkModel"/> that can be used to display pagination.
        /// </returns>
        IPageChainModel LinkPages(PageLinker pageLinker);
    }

    /// <summary>
    /// Interface for the results of a pagination request.
    /// </summary>
    /// <typeparam name="TSource">The type of element in the source from which the results came.</typeparam>
    public interface IPageResultsModel<TSource> : IPageResultsModel {

        /// <summary>
        /// The results of the page request.
        /// </summary>
        ISource<TSource> Results { get; }
    }

    /// <summary>
    /// Interface for the results of a pagination request.
    /// </summary>
    /// <typeparam name="TSource">The type of element in the source from which the results came.</typeparam>
    /// <typeparam name="TRequest">The type of request that was made to create the results.</typeparam>
    public interface IPageResultsModel<TSource, TRequest> : IPageResultsModel<TSource> where TRequest : IPageRequestModel {

        /// <summary>
        /// The source from which the results of the page request came.
        /// </summary>
        IOrderedPageSourceModel<TSource, TRequest> Source { get; }
    }
}
