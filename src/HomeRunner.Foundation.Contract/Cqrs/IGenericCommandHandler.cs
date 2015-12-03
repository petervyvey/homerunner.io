
using MediatR;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IGenericCommandHandler<in TCommand, out TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : ICommand<ICommandResult>, IRequest<TCommandResult>
        where TCommandResult : ICommandResult { }
}
