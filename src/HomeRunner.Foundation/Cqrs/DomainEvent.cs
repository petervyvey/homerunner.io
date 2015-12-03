
using System;

namespace HomeRunner.Foundation.Cqrs
{
    public abstract class DomainEvent
        : IDomainEvent
    {
        protected DomainEvent()
        {
            this.Id = Guid.NewGuid();
        }

        protected DomainEvent(Guid correlationId)
            : this()
        {
            this.CorrelationId = correlationId;
        }

        public Guid Id { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
