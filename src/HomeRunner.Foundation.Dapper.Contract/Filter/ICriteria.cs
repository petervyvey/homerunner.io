
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomeRunner.Foundation.Dapper.Filter
{
    public interface ICriteria
    {
        IList<ICriterion> Restrictions { get; }

        IList<ICriteria> AndCriteria { get; }

        IList<ICriteria> OrCriteria { get; }

        ICriteria Clear();

        ICriterion Add<TEntity>(Expression<Func<TEntity, object>> property);

        ICriterion Add<TEntity>(Expression<Func<TEntity, object>> property, Func<object> converter);

        ICriteria And(params ICriteria[] andCriteria);

        ICriteria Or(params ICriteria[] orCriteria);
    }
}