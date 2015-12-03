
using MediatR;

namespace HomeRunner.Foundation.Cqrs
{
    public interface ICommandHandler<in TCommand>
        : IGenericCommandHandler<TCommand, ICommandResult>
        where TCommand : ICommand<ICommandResult>, IRequest<ICommandResult> { }
}
