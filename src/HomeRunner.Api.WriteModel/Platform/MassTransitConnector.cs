using System.Threading.Tasks;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Logging;
using MassTransit;

namespace HomeRunner.Api.WriteModel.Platform
{
    public class MassTransitConnector
    {
        public static async Task SendCommand<TCommand>(TCommand command, IBus bus)
            where TCommand : ICommand<ICommandResult>
        {
            Logger.Log.Info(string.Format("Submiting command: {0} [{1}] ", command.GetType().FullName, command.Id));
            ISendEndpoint endpoint = await bus.GetSendEndpoint(Foundation.RabbitMQ.Configuration.Exchange);
            await endpoint.Send(new CommandMessage<TCommand>(command));

            Logger.Log.Info(string.Format("Command submitted: {0} [{1}]", command.GetType().FullName, command.Id));
        }
    }
}