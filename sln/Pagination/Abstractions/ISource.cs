using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Pagination.Abstractions {
    
    /// <summary>
    /// Interface for the underlying source for paged results.
    /// </summary>
    /// <typeparam name="T">The type of element in the source.</typeparam>
    public interface ISource<T> : IQueryable<T> {

        /// <summary>
        /// Gets the underlying <see cref="T:IQueryable{T}"/> object used to create this <see cref="T:ISource{T}"/>.
        /// </summary>
        IQueryable<T> Queryable { get; }

        /// <summary>
        /// Counts the total number of elements in this data source.
        /// </summary>
        /// <returns>The total number of elements in this data source.</returns>
        int Count();

        /// <summary>
        /// Returns a new source with the first <paramref name="count"/> number
        /// of elements from this source.
        /// </summary>
        /// <param name="count">The number of elements from this source to be returned.</param>
        /// <returns>
        /// A new source with the first <paramref name="count"/> number
        /// of elements from this source.
        /// </returns>
        ISource<T> Take(int count);

        /// <summary>
        /// Returns a new source whose elements are ordered by the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TKey">The return type of the key by which the elements are ordered.</typeparam>
        /// <param name="keySelector">The expression that returns the key by which the elements are ordered.</param>
        /// <returns>
        /// A new source whose elements are ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedSource<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);

        /// <summary>
        /// Returns a new source whose elements are descendingly ordered by the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TKey">The return type of the key by which the elements are descendingly ordered.</typeparam>
        /// <param name="keySelector">The expression that returns the key by which the elements are descendingly ordered.</param>
        /// <returns>
        /// A new source whose elements are descendingly ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedSource<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
    }
}
