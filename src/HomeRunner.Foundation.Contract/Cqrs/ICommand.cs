
using MediatR;
using System;

namespace HomeRunner.Foundation.Cqrs
{
    public interface ICommand<out TResult>
        : IRequest<TResult>, IWithIdentifier<Guid> { }
}
