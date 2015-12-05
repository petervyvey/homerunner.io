
using FluentValidation;
using HomeRunner.Domain.WriteModel.Commands;
using System;

namespace HomeRunner.Domain.Platform.Commands
{
    public class ClaimTaskActivityCommandValidator
        : AbstractValidator<ClaimTaskActivityCommand>
    {
        public ClaimTaskActivityCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("TASK ID equals Guid.Empty");

            RuleFor(c => c.TaskId).Must(this.BeAuthorized);
        }

        private bool BeAuthorized(Guid taskId)
        {
            return true;
        }
    }
}
