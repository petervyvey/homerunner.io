
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using MassTransit;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace HomeRunner.Domain.WriteModel
{
    public abstract class DomainEventMessagePublisher
            : IDomainEventMessagePublisher
    {
        protected static readonly ConcurrentDictionary<Type, Type> MESSAGE_TYPES = new ConcurrentDictionary<Type, Type>();

        public void Publish(IDomainEvent domainEvent)
        {
            this.Publish(new List<IDomainEvent> { domainEvent });
        }

        public abstract void Publish(IEnumerable<IDomainEvent> domainEvents);
    }

    public class BusDomainEventPublisher
        : DomainEventMessagePublisher
    {
        private readonly IBus bus;

        private readonly IDomainEventMessagePublisher inner;

        public BusDomainEventPublisher(IBus bus, IDomainEventMessagePublisher inner)
            : base()
        {
            this.bus = bus;
            this.inner = inner;
        }

        public override void Publish(IEnumerable<IDomainEvent> domainEvents)
        {
            List<IDomainEvent> _domainEvents = domainEvents.ToList();

            _domainEvents.ForEach(de =>
            {
                Logger.Log.Info(string.Format("Publishing domain event:\r\n{0}", de.ToJson()));

                Type messageType;
                if (!MESSAGE_TYPES.TryGetValue(de.GetType(), out messageType))
                {
                    messageType = MESSAGE_TYPES[de.GetType()] = typeof(DomainEventMessage<>).MakeGenericType(de.GetType());
                }

                var message = Activator.CreateInstance(messageType, de);
                this.bus.Publish(message);

                this.inner.Publish(_domainEvents.ToList());

                Logger.Log.Debug(string.Format("Published domain event:\r\n{0}", de.ToJson()));
            });
        }
    }

    public class LocalDomainEventPublisher
        : DomainEventMessagePublisher
    {
        private readonly IMediator mediator;

        public LocalDomainEventPublisher(IMediator mediator)
            : base()
        {
            this.mediator = mediator;
        }

        public override void Publish(IEnumerable<IDomainEvent> domainEvents)
        {
            domainEvents.ToList().ForEach(de =>
            {
                Logger.Log.Info(string.Format("Publishing domain event:\r\n{0}", de.ToJson()));

                Type messageType;
                if (!MESSAGE_TYPES.TryGetValue(de.GetType(), out messageType))
                {
                    messageType = MESSAGE_TYPES[de.GetType()] = typeof(DomainEventMessage<>).MakeGenericType(de.GetType());
                }

                INotification message = Activator.CreateInstance(messageType, de) as INotification;
                this.mediator.Publish(message);

                Logger.Log.Debug(string.Format("Published domain event:\r\n{0}", de.ToJson()));
            });
        }
    }
}
