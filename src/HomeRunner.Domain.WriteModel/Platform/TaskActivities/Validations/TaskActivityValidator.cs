
using Autofac;
using Autofac.Core;
using FluentValidation;
using FluentValidation.Results;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities;
using HomeRunner.Foundation.Entity;

namespace HomeRunner.Domain.WriteModel
{
	public class TaskActivityValidator
		: AbstractValidator<TaskActivity>
	{
		public TaskActivityValidator ()
		{
			this.RuleFor (x => x.Description).NotEmpty ();
		}
	}
}