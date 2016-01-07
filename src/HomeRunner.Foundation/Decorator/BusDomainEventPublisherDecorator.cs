﻿
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using HomeRunner.Foundation.MessageBus;
using MediatR;
using System;
using System.Linq;

namespace HomeRunner.Foundation.Decorator
{
    public class BusDomainEventPublisherDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : ICommand<TCommandResult>, IRequest<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
        private readonly IBusConnector connector;

        private readonly IRequestHandler<TCommand, TCommandResult> handler;

        public BusDomainEventPublisherDecorator(IBusConnector connector, IRequestHandler<TCommand, TCommandResult> handler)
        {
            this.connector = connector;
            this.handler = handler;
        }

        public TCommandResult Handle(TCommand command)
        {
            string _command = command.ToJson();

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "pipeline -> ", this.GetType().Name);
            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "received command", _command);
            TCommandResult events = this.handler.Handle(command);

            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "publish domain events to bus", _command);
            events.ToList().ForEach(de =>
            {
                var _domainEvent = de.ToJson();

                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "domain event", _domainEvent);

                // Try to get the message type.
                Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "find domain event type message type", de.GetType().FullName);
                Type messageType;
                if (!DomainEventPublisherTypeCache.MESSAGE_TYPES.TryGetValue(de.GetType(), out messageType))
                {
                    Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, command.Id, "domain event message type not found", de.GetType());

                    Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, command.Id, "create domain event message type", de.GetType());
                    messageType = DomainEventPublisherTypeCache.MESSAGE_TYPES[de.GetType()] = typeof(DomainEventMessage<>).MakeGenericType(de.GetType());

                    Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, command.Id, "domain event message type created", messageType.FullName);
                }
                Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, command.Id, "domain event message type found", messageType.Name);

                IDomainEventMessage<IDomainEvent> message = Activator.CreateInstance(messageType, de) as IDomainEventMessage<IDomainEvent>;

                // Publish the message on the bus
                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "publish domain event message", message.ToJson());
                this.connector.PublishEvent(message);

                Logger.Log.DebugFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "domain event published", _domainEvent);
            });

            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, command.Id, "publish domain events to bus completed", _command);

            return events;
        }
    }
}
