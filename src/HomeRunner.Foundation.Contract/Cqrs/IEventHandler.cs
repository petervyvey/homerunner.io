
using MediatR;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IEventHandler<in TDomainEventMessage>
        : INotificationHandler<TDomainEventMessage>
        where TDomainEventMessage : IDomainEventMessage<IDomainEvent> { }
}
