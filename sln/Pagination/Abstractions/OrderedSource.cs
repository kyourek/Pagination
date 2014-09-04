using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

namespace Pagination.Abstractions {
    
    internal class OrderedSource<T> : Source<T>, IOrderedSource<T>, IOrderedQueryable<T> {

        private IOrderedQueryable<T> OrderedQueryable { get { return (IOrderedQueryable<T>)Queryable; } }

        public OrderedSource(IOrderedQueryable<T> orderedQueryable) : base(orderedQueryable) { }

        public virtual ISource<T> Skip(int count) {
            return new Source<T>(OrderedQueryable.Skip(count));
        }

        public virtual IOrderedSource<T> ThenBy<TKey>(Expression<Func<T, TKey>> keySelector) {
            return new OrderedSource<T>(OrderedQueryable.ThenBy(keySelector));
        }

        public virtual IOrderedSource<T> ThenByDescending<TKey>(Expression<Func<T, TKey>> keySelector) {
            return new OrderedSource<T>(OrderedQueryable.ThenByDescending(keySelector));
        }

        IOrderedQueryable<T> IOrderedSource<T>.Queryable {
            get { return OrderedQueryable; }
        }
    }
}
