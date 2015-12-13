
using FluentValidation;
using FluentValidation.Results;
using System;

namespace HomeRunner.Foundation.Entity
{
	public interface IWithValidator<TDomainEntity> 
		: IDomainEntity 
		where TDomainEntity: IDomainEntity
	{
		ValidationResult Validate();
	}

}

