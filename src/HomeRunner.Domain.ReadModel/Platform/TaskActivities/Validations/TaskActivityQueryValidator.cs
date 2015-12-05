
using FluentValidation;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using System;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities.Validations
{
    public class TaskActivityQueryValidator
        : AbstractValidator<TaskActivityQuery>
    {
        public TaskActivityQueryValidator()
        {
            RuleFor(c => c.TaskActivityId).NotEmpty().WithMessage("TASK ID equals Guid.Empty");
            RuleFor(c => c.TaskActivityId).Must(this.BeAuthorized);
        }

        private bool BeAuthorized(Guid taskId)
        {
            return true;
        }
    }
}
