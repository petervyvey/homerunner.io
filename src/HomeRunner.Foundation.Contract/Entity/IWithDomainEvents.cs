
using HomeRunner.Foundation.Cqrs;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Entity
{
    public interface IWithDomainEvents
    {
        List<IDomainEvent> DomainEvents { get; }
    }
}
