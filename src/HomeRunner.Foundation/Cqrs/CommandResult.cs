
using System;
using System.Linq;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Cqrs
{
    public class CommandResult
        : List<IDomainEvent>, ICommandResult
    {
        public CommandResult(Guid correlationId, IDomainEvent domainEvent)
            : this(correlationId, new List<IDomainEvent> { domainEvent }) { }

        public CommandResult(Guid correlationId, IEnumerable<IDomainEvent> domainEvents)
        {
            (domainEvents ?? new List<IDomainEvent>())
                .ToList()
                .ForEach(de =>
                {
                    de.CorrelationId = correlationId;
                    this.Add(de);
                });
        }
    }
}
