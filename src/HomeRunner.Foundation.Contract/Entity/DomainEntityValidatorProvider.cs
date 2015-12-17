
using Autofac;
using FluentValidation;

namespace HomeRunner.Foundation.Entity
{
	public class DomainEntityValidatorProvider
        : IDomainEntityValidatorProvider 
	{
		private readonly IComponentContext context;

		public DomainEntityValidatorProvider(IComponentContext context)
		{
			this.context = context;
		}

		public IValidator<TDomainEntity> Create<TDomainEntity>() 
			where TDomainEntity: IDomainEntity
		{  
			return this.context.Resolve<IValidator<TDomainEntity>>();
		}  
	}
}

