
using MediatR;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IDomainEventMessage<out TDomainEvent>
        : INotification
        where TDomainEvent : IDomainEvent
    {
        TDomainEvent DomainEvent { get; }
    }
}
