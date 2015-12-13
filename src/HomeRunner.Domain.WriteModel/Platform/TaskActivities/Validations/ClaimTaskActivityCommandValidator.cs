
using FluentValidation;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using System;

namespace HomeRunner.Domain.Platform.Commands
{
    public class ClaimTaskActivityCommandValidator
        : AbstractValidator<ClaimTaskActivityCommand>
    {
        public ClaimTaskActivityCommandValidator()
        {
			this.RuleFor(c => c.Id).NotEmpty().WithMessage("COMMAND ID equals Guid.Empty");
			this.RuleFor(c => c.TaskId).NotEmpty().WithMessage("TASK ID equals Guid.Empty");
			this.RuleFor(c => c.TaskId).Must(this.BeAuthorized);
        }

        private bool BeAuthorized(Guid taskId)
        {
            return true;
        }
    }
}
