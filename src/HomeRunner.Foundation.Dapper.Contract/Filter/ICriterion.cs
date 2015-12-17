
using System;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Dapper.Filter
{
    public interface ICriterion
    {
        ICriteria Criteria { get; set; }

        string Field { get; set; }

        Type FieldType { get; set; }

        Operator Operator { get; set; }

        object Value { get; set; }

        ICriteria Equal(object value);

        ICriteria LessThan(object value);

        ICriteria LessThanOrEqual(object value);

        ICriteria GreaterThan(object value);

        ICriteria GreaterThanOrEqual(object value);

        ICriteria In(IEnumerable<object> value);

        string ToString();
    }
}
