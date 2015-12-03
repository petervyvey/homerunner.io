
using MediatR;
using System;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IDomainEvent
        : IWithIdentifier<Guid>, IWithCorrelationIdentifier<Guid> { }
}
