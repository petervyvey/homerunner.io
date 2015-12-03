
using System.Collections.Generic;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IDomainEventMessagePublisher
    {
        void Publish(IDomainEvent domainEvent);

        void Publish(IEnumerable<IDomainEvent> domainEvents);
    }
}
