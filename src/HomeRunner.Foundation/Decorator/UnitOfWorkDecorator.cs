
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using HomeRunner.Foundation.Infrastructure.Transaction;
using MediatR;
using System;

namespace HomeRunner.Foundation.Decorator
{
    public class UnitOfWorkDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>, IRequest<TCommandResult>
        where TCommandResult : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRequestHandler<TCommand, TCommandResult> handler;

        public UnitOfWorkDecorator(IUnitOfWork unitOfWork, IRequestHandler<TCommand, TCommandResult> handler)
        {
            this.unitOfWork = unitOfWork;
            this.handler = handler;
        }

        public TCommandResult Handle(TCommand command)
        {
            string _command = command.ToJson();
            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "pipeline -> ", this.GetType().Name);
            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "received command", _command);

            TCommandResult events = default(TCommandResult);
            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "starting unit of work", unitOfWork.GetType().FullName);
            this.unitOfWork.Start(() =>
            {
                Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, command.Id, "unit of work started", unitOfWork.GetType().FullName);
                events = this.handler.Handle(command);

                Logger.Log.DebugFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "unit of work handled command", _command);
            }, (command is IWithIdentifier<Guid>) ? ((IWithIdentifier<Guid>)command).Id.ToString() : string.Empty);

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "unit of work completed", unitOfWork.GetType().FullName);

            return events;
        }
    }
}
