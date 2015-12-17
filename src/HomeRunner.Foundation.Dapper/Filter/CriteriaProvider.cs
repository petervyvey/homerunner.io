
using Autofac;

namespace HomeRunner.Foundation.Dapper.Filter
{
    public class CriteriaProvider
        : ICriteriaProvider
    {
        private readonly IComponentContext context;

        public CriteriaProvider(IComponentContext context)
		{
			this.context = context;
		}

        public ICriteria CreateCriteria()
        {
            return this.context.Resolve<ICriteria>();
        }

        public ICriterion CreateCriterion()
        {
            return this.context.Resolve<ICriterion>();
        }
    }
}
