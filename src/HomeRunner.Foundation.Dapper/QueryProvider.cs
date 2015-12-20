
using Autofac;
using HomeRunner.Foundation.Dapper.Filter;
using System;

namespace HomeRunner.Foundation.Dapper
{
    public delegate ICriteria CriteriaFactory(Type serviceType);

    public delegate ICriterion CriterionFactory(Type serviceType);

    public class QueryProvider
		: IQueryProvider
    {
        private readonly IComponentContext context;

        public QueryProvider(IComponentContext context)
        {
            this.context = context;
        }

        public ICriteria<TEntity> From<TEntity>() where TEntity : class
        {
            return this.context.Resolve<CriteriaFactory>().Invoke(typeof(TEntity)) as ICriteria<TEntity>;
        }

		public ICriterion<TEntity> CreateCriterion<TEntity>(string prefix) where TEntity : class
        {
			var criterion = this.context.Resolve<ICriterion<TEntity>>();
			criterion.Prefix = prefix;

			return criterion;
        }
    }
}
