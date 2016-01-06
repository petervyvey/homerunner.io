
using HomeRunner.Foundation.Cqrs;
using System.Threading.Tasks;

namespace HomeRunner.Foundation.MessageBus
{
    public interface IBusConnector
    {
        Task SendCommand<TCommand>(TCommand command)
            where TCommand : ICommand<ICommandResult>;

        Task PublishEvent<TDomainEventMessage>(TDomainEventMessage domainEventMessage)
            where TDomainEventMessage : class, IDomainEventMessage<IDomainEvent>;
    }
}
