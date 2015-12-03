
using System.Collections.Generic;

namespace HomeRunner.Foundation.Cqrs
{
    public interface ICommandResult
        : IList<IDomainEvent> { }
}
