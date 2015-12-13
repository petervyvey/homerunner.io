
using FluentValidation;

namespace HomeRunner.Foundation.Entity
{
	public interface IDomainEntityValidatorFactory
	{
		IValidator<TDomainEntity> Create<TDomainEntity> () where TDomainEntity: IDomainEntity;
	}
}

