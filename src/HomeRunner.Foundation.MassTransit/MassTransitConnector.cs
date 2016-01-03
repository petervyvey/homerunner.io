
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
            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, command.Id, "submiting command", command.GetType().FullName);
            ISendEndpoint endpoint = await this.bus.GetSendEndpoint(Configuration.RabbitMQ.CommandExchangeUri);
            await endpoint.Send(new CommandMessage<TCommand>(command));

            Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, command.Id, "command submitted", command.GetType().FullName);
        }
    }
}
