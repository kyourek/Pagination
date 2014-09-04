using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Pagination.Abstractions;

namespace Pagination.Models {

    /// <summary>
    /// Interface for models of an ordered source that can be paginated.
    /// </summary>
    /// <typeparam name="TSource">The type of element in the source.</typeparam>
    public interface IOrderedPageSourceModel<TSource> : IPageSourceModel, IPageSourceModel<TSource> {

        /// <summary>
        /// Gets the underlying ordered source of this source.
        /// </summary>
        new IOrderedSource<TSource> Source { get; }
    }

    /// <summary>
    /// Interface for models of an ordered source that can be paginated.
    /// </summary>
    /// <typeparam name="TSource">The type of element in the source.</typeparam>
    /// <typeparam name="TRequest">The type of the request made that created the source.</typeparam>
    public interface IOrderedPageSourceModel<TSource, TRequest> : IPageSourceModel, IPageSourceModel<TSource>, IPageSourceModel<TSource, TRequest>, IOrderedPageSourceModel<TSource> where TRequest : IPageRequestModel {

        /// <summary>
        /// Adds additional ordering to this source.
        /// </summary>
        /// <typeparam name="TKey">The type by which the additional ordering is ordered.</typeparam>
        /// <param name="keySelector">The expression that returns the additional ordering.</param>
        /// <returns>
        /// A new source that is additionally ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedPageSourceModel<TSource, TRequest> ThenBy<TKey>(Expression<Func<TSource, TKey>> keySelector);

        /// <summary>
        /// Adds additional, descending ordering to this source.
        /// </summary>
        /// <typeparam name="TKey">The type by which the additional descending ordering is ordered.</typeparam>
        /// <param name="keySelector">The expression that returns the additional descending ordering.</param>
        /// <returns>
        /// A new source that is additionally, descendingly ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedPageSourceModel<TSource, TRequest> ThenByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector);

        /// <summary>
        /// Creates a paginated results model from this paginated source.
        /// </summary>
        /// <returns>A new paginated results model from this paginated source, given its current ordering.</returns>
        IPageResultsModel<TSource, TRequest> Query();

        /// <summary>
        /// Returns a new ordered page source with the specified maximum number of items displayed on a page.
        /// </summary>
        /// <param name="value">The maximum number of items that can be displayed on a page.</param>
        /// <returns>A new ordered page source with the specified maximum number of items displayed on a page.</returns>
        new IOrderedPageSourceModel<TSource, TRequest> MaxItemsPerPage(int value);

        /// <summary>
        /// Returns a new ordered page source with the specified default number of items displayed on a page.
        /// </summary>
        /// <param name="value">The default number of items that are displayed on a page.</param>
        /// <returns>A new ordered page source with the specified default number of items displayed on a page.</returns>
        new IOrderedPageSourceModel<TSource, TRequest> DefaultItemsPerPage(int value);
    }
}
