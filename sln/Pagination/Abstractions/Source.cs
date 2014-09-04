using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

namespace Pagination.Abstractions {

    internal class Source<T> : ISource<T>, IQueryable<T> {

        public IQueryable<T> Queryable { get { return _Queryable; } }
        private readonly IQueryable<T> _Queryable;

        public Source(IQueryable<T> queryable) {
            if (null == queryable) throw new ArgumentNullException("queryable");
            _Queryable = queryable;
        }

        public virtual int Count() {
            return Queryable.Count();
        }

        public virtual ISource<T> Take(int count) {
            return new Source<T>(Queryable.Take(count));
        }

        public virtual IOrderedSource<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector) {
            return new OrderedSource<T>(Queryable.OrderBy(keySelector));
        }

        public virtual IOrderedSource<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector) {
            return new OrderedSource<T>(Queryable.OrderByDescending(keySelector));
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Queryable.GetEnumerator();
        }

        Type IQueryable.ElementType {
            get { return Queryable.ElementType; }
        }

        Expression IQueryable.Expression {
            get { return Queryable.Expression; }
        }

        IQueryProvider IQueryable.Provider {
            get { return Queryable.Provider; }
        }
    }
}
