using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Pagination.Abstractions;

namespace Pagination.Models {

    /// <summary>
    /// Interface for a source that can be accessed with a pagination request.
    /// </summary>
    public interface IPageSourceModel {

        /// <summary>
        /// Gets the maximum number of items that can be displayed on a page.
        /// </summary>
        /// <returns>The maximum number of items that can be displayed on a page.</returns>
        int GetMaxItemsPerPage();

        /// <summary>
        /// Gets the default number of items that are displayed on a page.
        /// </summary>
        /// <returns>The default number of items that are displayed on a page.</returns>
        int GetDefaultItemsPerPage();
    }

    /// <summary>
    /// Interface for models of a source that can be paginated.
    /// </summary>
    /// <typeparam name="TSource">The type of element in the source.</typeparam>
    public interface IPageSourceModel<TSource> : IPageSourceModel {

        /// <summary>
        /// Gets the source of elements available to be paginated.
        /// </summary>
        ISource<TSource> Source { get; }
    }

    /// <summary>
    /// Interface for a source that can be accessed with a pagination request.
    /// </summary>
    /// <typeparam name="TSource">The type of element in the source.</typeparam>
    /// <typeparam name="TRequest">The type of request that created this page source.</typeparam>
    public interface IPageSourceModel<TSource, TRequest> : IPageSourceModel, IPageSourceModel<TSource> where TRequest : IPageRequestModel {

        /// <summary>
        /// The request that caused this page source to be created.
        /// </summary>
        TRequest Request { get; }

        /// <summary>
        /// Returns a new page source with the specified maximum number of items displayed on a page.
        /// </summary>
        /// <param name="value">The maximum number of items that can be displayed on a page.</param>
        /// <returns>A new page source with the specified maximum number of items displayed on a page.</returns>
        IPageSourceModel<TSource, TRequest> MaxItemsPerPage(int value);

        /// <summary>
        /// Returns a new page source with the specified default number of items displayed on a page.
        /// </summary>
        /// <param name="value">The default number of items that are displayed on a page.</param>
        /// <returns>A new page source with the specified default number of items displayed on a page.</returns>
        IPageSourceModel<TSource, TRequest> DefaultItemsPerPage(int value);

        /// <summary>
        /// Orders this source using the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of value on which the source is to be ordered.</typeparam>
        /// <param name="keySelector">The expression by which the source is to be ordered.</param>
        /// <returns>
        /// An ordered page source that contains the elements in this source ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedPageSourceModel<TSource, TRequest> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector);

        /// <summary>
        /// Orders in a descending manner this source using the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of value on which the source is to be descendingly ordered.</typeparam>
        /// <param name="keySelector">The expression by which the source is to be descendingly ordered.</param>
        /// <returns>
        /// An ordered page source that contains the elements in this source descendingly ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedPageSourceModel<TSource, TRequest> OrderByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector);
    }
}
