
using FluentValidation;

namespace HomeRunner.Foundation.Entity
{
	public interface IDomainEntityValidatorProvider
	{
		IValidator<TDomainEntity> Create<TDomainEntity> () where TDomainEntity: IDomainEntity;
	}
}

