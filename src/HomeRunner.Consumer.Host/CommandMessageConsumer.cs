
using HomeRunner.Domain.WriteModel.Commands;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using MassTransit;
using MediatR;
using System.Threading.Tasks;

namespace HomeRunner.Consumer.Host
{
    public class CommandMessageConsumer :
        IConsumer<CommandMessage<ClaimTaskActivityCommand>>,
        IConsumer<CommandMessage<UnclaimTaskActivityCommand>>
    {
        private readonly IMediator mediator;

        private readonly IDomainEventMessagePublisher publisher;

        public CommandMessageConsumer() { }

        public CommandMessageConsumer(IMediator mediator, IDomainEventMessagePublisher publisher)
            : this()
        {
            Logger.Log.Info(string.Format("Creating consumer: {0}", this.GetType().AssemblyQualifiedName));
            this.mediator = mediator;
            this.publisher = publisher;

            Logger.Log.Debug(string.Format("Created consumer: {0}", this.GetType().AssemblyQualifiedName));
        }

        public Task Consume(ConsumeContext<CommandMessage<ClaimTaskActivityCommand>> context)
        {
            return ConsumeCommand(context);
        }

        public Task Consume(ConsumeContext<CommandMessage<UnclaimTaskActivityCommand>> context)
        {
            return ConsumeCommand(context);
        }

        private Task ConsumeCommand<TCommand>(ConsumeContext<CommandMessage<TCommand>> context)
            where TCommand : ICommand<ICommandResult>
        {
            return Task.Run(() =>
            {
                var _message = context.Message.ToJson();

                Logger.Log.InfoFormat(string.Format("Consuming message:\r\n{0}", context.Message));
                ICommandResult events = this.mediator.Send(context.Message.Command);

                Logger.Log.Debug(string.Format("Consumed message:\r\n{0}", _message));
                Logger.Log.Info(string.Format("Received domain events:\r\n{0}", events.ToJson()));

                //this.publisher.Publish(events);
            });
        }
    }
}
