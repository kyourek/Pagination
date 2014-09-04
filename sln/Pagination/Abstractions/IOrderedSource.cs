using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Pagination.Abstractions {

    /// <summary>
    /// Interface for an ordered queryable source of elements.
    /// </summary>
    /// <typeparam name="T">The type of element in the source.</typeparam>
    public interface IOrderedSource<T> : ISource<T>, IOrderedQueryable<T> {

        /// <summary>
        /// Gets the underlying <see cref="T:IOrderedQueryable{T}"/> of this <see cref="T:IOrderedSource{T}"/>.
        /// </summary>
        new IOrderedQueryable<T> Queryable { get; }
        
        /// <summary>
        /// Returns an <see cref="T:ISource{T}"/> that contains all elements in this <see cref="T:IOrderedSource{T}"/>
        /// after skipping the specified <paramref name="count"/> number of elements.
        /// </summary>
        /// <param name="count">The number of elements in this <see cref="T:IOrderedSource{T}"/> to be skipped.</param>
        /// <returns>
        /// An <see cref="T:ISource{T}"/> that contains all elements in this <see cref="T:IOrderedSource{T}"/>
        /// after skipping the specified <paramref name="count"/> number of elements.
        /// </returns>
        ISource<T> Skip(int count);

        /// <summary>
        /// Returns an <see cref="T:IOrderedSource{T}"/> that is ordered by this <see cref="T:IOrderedSource{T}"/>
        /// and then additionally ordered by the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of object by which this <see cref="T:IOrderedSource{T}"/> is to be additionally ordered.
        /// </typeparam>
        /// <param name="keySelector">
        /// An <see cref="Expression{Func}"/> that returns the property by which this <see cref="T:IOrderedSource{T}"/>
        /// is to be additionally ordered.
        /// </param>
        /// <returns>
        /// An <see cref="T:IOrderedSource{T}"/> that is ordered by this <see cref="T:IOrderedSource{T}"/>
        /// and then additionally ordered by the <paramref name="keySelector"/>.
        /// </returns>
        IOrderedSource<T> ThenBy<TKey>(Expression<Func<T, TKey>> keySelector);

        /// <summary>
        /// Returns an <see cref="T:IOrderedSource{T}"/> that is ordered by this <see cref="T:IOrderedSource{T}"/>
        /// and then additionally ordered by the <paramref name="keySelector"/> descending.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of object by which this <see cref="T:IOrderedSource{T}"/> is to be additionally descendingly ordered.
        /// </typeparam>
        /// <param name="keySelector">
        /// An <see cref="Expression{Func}"/> that returns the property by which this <see cref="T:IOrderedSource{T}"/>
        /// is to be additionally descendingly ordered.
        /// </param>
        /// <returns>
        /// An <see cref="T:IOrderedSource{T}"/> that is ordered by this <see cref="T:IOrderedSource{T}"/>
        /// and then additionally ordered by the <paramref name="keySelector"/> descending.
        /// </returns>
        IOrderedSource<T> ThenByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
    }
}
