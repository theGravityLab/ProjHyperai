using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiteDB;

namespace HyperaiShell.App.Data
{
    public class LiteDbQueryable<T> : IQueryable<T>
    {
        internal readonly ILiteQueryable<T> Queryable;

        public LiteDbQueryable(ILiteQueryable<T> queryable)
        {
            Queryable = queryable;
            Expression = Expression.Constant(queryable);
        }

        public Type ElementType => typeof(T);

        public Expression Expression { get; }

        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            return Queryable.ToEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queryable.ToEnumerable().GetEnumerator();
        }
    }
}
