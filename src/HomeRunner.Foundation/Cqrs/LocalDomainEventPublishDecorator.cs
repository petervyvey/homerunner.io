
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace HomeRunner.Foundation.Cqrs
{
    public class LocalDomainEventPublishDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : IRequest<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
        protected static readonly ConcurrentDictionary<Type, Type> MESSAGE_TYPES = new ConcurrentDictionary<Type, Type>();

        private readonly IMediator mediator;

        private readonly IRequestHandler<TCommand, TCommandResult> handler;

        public LocalDomainEventPublishDecorator(IMediator mediator, IRequestHandler<TCommand, TCommandResult> handler)
        {
            this.mediator = mediator;
            this.handler = handler;
        }

        public TCommandResult Handle(TCommand command)
        {
            string _command = command.ToJson();

            LogInstance.Log.Info(string.Format("Handling command:\r\n{0}", _command));
            TCommandResult events = this.handler.Handle(command);

            events.ToList().ForEach(de =>
            {
                LogInstance.Log.Info(string.Format("Publishing domain event:\r\n{0}", de.ToJson()));

                Type messageType;
                if (!MESSAGE_TYPES.TryGetValue(de.GetType(), out messageType))
                {
                    messageType = MESSAGE_TYPES[de.GetType()] = typeof(DomainEventMessage<>).MakeGenericType(de.GetType());
                }

                INotification message = Activator.CreateInstance(messageType, de) as INotification;
                this.mediator.Publish(message);

                LogInstance.Log.Debug(string.Format("Published domain event:\r\n{0}", de.ToJson()));
            });

            LogInstance.Log.Debug(string.Format("Command handled:\r\n{0}", _command));

            return events;
        }
    }
}
