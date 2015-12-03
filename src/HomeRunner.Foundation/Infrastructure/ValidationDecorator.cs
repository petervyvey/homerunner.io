
using FluentValidation;
using FluentValidation.Results;
using HomeRunner.Foundation.Extension;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace HomeRunner.Foundation.Infrastructure
{
    public class ValidationDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : IRequest<TCommandResult>
        where TCommandResult : class
    {
        private readonly IRequestHandler<TCommand, TCommandResult> handler;

        private readonly IValidator<TCommand>[] validators;

        public ValidationDecorator(IValidator<TCommand>[] validators, IRequestHandler<TCommand, TCommandResult> handler)
        {
            this.validators = validators;
            this.handler = handler;
        }

        public TCommandResult Handle(TCommand request)
        {
            ValidationContext context = new ValidationContext(request);

            List<ValidationFailure> failures =
                this.validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

            if (failures.Any()) throw new ValidationException((new {Request = request, Failures = failures}).ToJson(), failures);

            return this.handler.Handle(request);
        }
    }
}
