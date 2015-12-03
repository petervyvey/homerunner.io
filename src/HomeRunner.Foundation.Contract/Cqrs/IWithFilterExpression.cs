
using HomeRunner.Foundation.Entity;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IWithFilterExpression<TDataEntity>
        where TDataEntity: IDataEntity
    {
        [JsonIgnore]
        Expression<Func<TDataEntity, bool>> FilterExpression { get; }
    }
}
