
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace HomeRunner.Foundation.NHibernate.Contract
{
    public abstract class QueryProvider<TSession>
        : IQueryProvider<TSession>
        where TSession : ISession
    {
        protected QueryProvider(TSession session)
        {
            this.Session = session;
        }

        /// <summary>
        /// The <see cref="IUnitOfWork"/> session instance.
        /// </summary>
        public TSession Session { get; private set; }

        public virtual IQueryable<TEntity> CreateQuery<TEntity>()
        {
            IQueryable<TEntity> queryable = this.Session.Query<TEntity>();

            return queryable;
        }

        public TEntity SingleOrDefault<TWithFilterExpression, TEntity>(TWithFilterExpression query)
            where TWithFilterExpression : IWithFilterExpression<TEntity>, Cqrs.IQuery
            where TEntity : class, Entity.IDataEntity
        {
            TEntity entity = this.CreateQuery<TEntity>().SingleOrDefault(query.FilterExpression);

            return entity;
        }

        public IQueryable<TEntity> Find<TWithFilterExpression, TEntity>(TWithFilterExpression query)
            where TWithFilterExpression : IWithFilterExpression<TEntity>, Cqrs.IQuery
            where TEntity : class, Entity.IDataEntity
        {
            IQueryable<TEntity> entity = this.CreateQuery<TEntity>().Where(query.FilterExpression);

            return entity;
        }
    }
}
