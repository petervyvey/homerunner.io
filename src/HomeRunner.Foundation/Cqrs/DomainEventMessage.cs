
namespace HomeRunner.Foundation.Cqrs
{
    public class DomainEventMessage<TDomainEvent>
        : IDomainEventMessage<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        public DomainEventMessage(TDomainEvent domanEvent)
        {
            this.DomainEvent = domanEvent;
        }

        public TDomainEvent DomainEvent { get; private set; }
    }
}
