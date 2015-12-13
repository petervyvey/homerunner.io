
using FluentValidation.Results;
using FluentValidation;
using HomeRunner.Foundation.Entity;
using System;

namespace HomeRunner.Foundation.Extension
{
	public static class DomainEntityExtension
	{
		public static IDomainEntity IfValid<TDomainEntity>(this TDomainEntity instance, Action<TDomainEntity> inCaseOfValid)
			where TDomainEntity: IDomainEntity
		{
			if (instance is IWithValidator<TDomainEntity> ) 
			{
				ValidationResult result = ((IWithValidator<TDomainEntity> )instance).Validate();
				if (result.IsValid) 
				{
					inCaseOfValid (instance);
				} 
			}

			return instance;
		}

		public static IDomainEntity MustBeValid<TDomainEntity>(this TDomainEntity instance)
			where TDomainEntity: IDomainEntity
		{
			return instance.ElseThrow();
		}

		public static IDomainEntity ElseThrow<TDomainEntity>(this TDomainEntity instance) 
			where TDomainEntity: IDomainEntity
		{
			if(instance is IWithValidator<TDomainEntity> )
			{
				ValidationResult result = ((IWithValidator<TDomainEntity> )instance).Validate();
				if(result.IsValid)
				{
					throw new ValidationException (result.Errors);
				}
			}

			return instance;
		}
	}
}

