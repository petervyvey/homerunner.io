
using System;
using FluentValidation;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities.Validations
{
    class TaskActivityListQueryValidator
        : AbstractValidator<TaskActivityListQuery>
    {
        public TaskActivityListQueryValidator()
        {
            RuleFor(c => c.Identifiers).NotEmpty().WithMessage("TASK IDENTIFIERS is NULL");
            RuleFor(c => c.Identifiers).Must(this.BeAuthorized);
        }

        private bool BeAuthorized(Guid[] identifiers)
        {
            return true;
        }
    }
}
