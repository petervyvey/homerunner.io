
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace HomeRunner.Foundation.Decorator
{
    public class InternalDomainEventPublisherDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>, IRequest<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
        protected static readonly ConcurrentDictionary<Type, Type> MESSAGE_TYPES = new ConcurrentDictionary<Type, Type>();

        private readonly IMediator mediator;

        private readonly IRequestHandler<TCommand, TCommandResult> handler;

        public InternalDomainEventPublisherDecorator(IMediator mediator, IRequestHandler<TCommand, TCommandResult> handler)
        {
            this.mediator = mediator;
            this.handler = handler;
        }

        public TCommandResult Handle(TCommand command)
        {
            string _command = command.ToJson();

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "pipeline -> ", this.GetType().Name);
            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "received command", _command);
            TCommandResult events = this.handler.Handle(command);

            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "internal publish domain events", _command);
            events.ToList().ForEach(de =>
            {
                var _domainEvent = de.ToJson();

                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "publish domain event", _domainEvent);

                Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "find domain event type message type", de.GetType());
                Type messageType;
                if (!MESSAGE_TYPES.TryGetValue(de.GetType(), out messageType))
                {
                    Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, command.Id, "domain event message type not found", de.GetType());

                    Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, command.Id, "create domain event type", de.GetType());
                    messageType = MESSAGE_TYPES[de.GetType()] = typeof(DomainEventMessage<>).MakeGenericType(de.GetType());

                    Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, command.Id, "domain event message type created", messageType.FullName);
                }
                Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, command.Id, "domain event message type found", messageType.FullName);

                INotification message = Activator.CreateInstance(messageType, de) as INotification;

                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "publish domain event message", message.ToJson());
                this.mediator.Publish(message);

                Logger.Log.DebugFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "domain event published", _domainEvent);
            });

            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "internal publish domain events completed", _command);

            return events;
        }
    }
}
