
using MediatR;
using System;

namespace HomeRunner.Foundation.Cqrs
{
    public interface ICommand
        : IWithIdentifier<Guid> { }

    public interface ICommand<out TResult>
        : ICommand, IRequest<TResult> { }
}
