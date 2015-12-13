
using Autofac;
using FluentValidation;

namespace HomeRunner.Foundation.Entity
{
	public class DomainEntityValidatorFactory 
		: IDomainEntityValidatorFactory  
	{
		private readonly IComponentContext context;

		public DomainEntityValidatorFactory(IComponentContext context)
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

