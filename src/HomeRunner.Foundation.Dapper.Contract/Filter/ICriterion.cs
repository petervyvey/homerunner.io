
using System;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Dapper.Filter
{
    public interface ICriterion
    {
        string Field { get; set; }

        Type FieldType { get; set; }

        Operator Operator { get; set; }

        object Value { get; set; }
    }

    public interface ICriterion<TEntity>
        : ICriterion
        where TEntity : class
    {
		string Prefix { get; set; }

        ICriteria<TEntity> Criteria { get; set; }

        ICriteria<TEntity> EqualTo(object value);

        ICriteria<TEntity> LessThan(object value);

        ICriteria<TEntity> LessThanOrEqual(object value);

        ICriteria<TEntity> GreaterThan(object value);

        ICriteria<TEntity> GreaterThanOrEqual(object value);

        ICriteria<TEntity> In(IEnumerable<object> value);

		ICriteria<TEntity> Like(string value);

        string ToString();
    }
}
