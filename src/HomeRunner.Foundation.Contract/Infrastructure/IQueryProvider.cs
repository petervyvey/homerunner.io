﻿
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using NHibernate;
using System.Linq;

namespace HomeRunner.Foundation.Infrastructure
{
    public interface IQueryProvider<TSession> 
        where TSession: ISession
        //where TResult : class
    {
        TSession Session { get; }

        IQueryable<TEntity> CreateQuery<TEntity>();

        TEntity SingleOrDefault<TWithFilterExpression, TEntity>(TWithFilterExpression query)
            where TWithFilterExpression : IWithFilterExpression<TEntity>, Cqrs.IQuery
            where TEntity : class, IDataEntity;

        IQueryable<TEntity> Find<TWithFilterExpression, TEntity>(TWithFilterExpression query)
            where TWithFilterExpression : IWithFilterExpression<TEntity>, Cqrs.IQuery
            where TEntity : class, IDataEntity;
    }
}
