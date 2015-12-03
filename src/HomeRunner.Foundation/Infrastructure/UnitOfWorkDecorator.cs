
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using MediatR;
using System;

namespace HomeRunner.Foundation.Infrastructure
{
    public class UnitOfWorkDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : IRequest<TCommandResult>
        where TCommandResult: class 
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

            TCommandResult events = default(TCommandResult);
            this.unitOfWork.Start(() =>
            {
                LogInstance.Log.Info(string.Format("Handling command in {0}: \r\n{1}", this.unitOfWork.GetType().AssemblyQualifiedName, _command));
                events = this.handler.Handle(command);

                LogInstance.Log.Debug(string.Format("Handled command in {0}: \r\n{1}", this.unitOfWork.GetType().AssemblyQualifiedName, _command));
            }, (command is IWithIdentifier<Guid>) ? ((IWithIdentifier<Guid>)command).Id.ToString() : string.Empty);

            return events;
        }
    }
}
