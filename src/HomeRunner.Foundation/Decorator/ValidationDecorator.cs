
using FluentValidation;
using FluentValidation.Results;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace HomeRunner.Foundation.Decorator
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
            string _request = request.ToJson();
            string correlationId = request is ICommand ? (request as ICommand).Id.ToString() : "__UNSPECIFIED__";

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "pipeline -> ", this.GetType().Name);
            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "received command", _request);

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "validation started", this.GetType().FullName);
            ValidationContext context = new ValidationContext(request);

            List<ValidationFailure> failures =
                this.validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

            if (failures.Any()) throw new ValidationException((new { Request = request, Failures = failures }).ToJson(), failures);

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "validation completed", this.GetType().FullName);

            TCommandResult events = this.handler.Handle(request);

            return events;
        }
    }
}
