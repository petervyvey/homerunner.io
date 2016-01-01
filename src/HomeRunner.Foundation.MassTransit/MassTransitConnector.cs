
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Logging;
using HomeRunner.Foundation.MessageBus;
using MassTransit;
using System.Threading.Tasks;

namespace HomeRunner.Foundation.MassTransit
{
    public class MassTransitConnector
        : IBusConnector
    {
        private readonly IBus bus;

        public MassTransitConnector(IBus bus)
        {
            this.bus = bus;
        }

        public async Task SendCommand<TCommand>(TCommand command)
            where TCommand : ICommand<ICommandResult>
        {
            Logger.Log.Info(string.Format("[{0}] submiting command: {1}  ", command.Id, command.GetType().FullName));
            ISendEndpoint endpoint = await this.bus.GetSendEndpoint(Foundation.RabbitMQ.Configuration.Exchange);
            await endpoint.Send(new CommandMessage<TCommand>(command));

            Logger.Log.Info(string.Format("[{0}] command submitted: {1} ", command.Id, command.GetType().FullName));
        }
    }
}
