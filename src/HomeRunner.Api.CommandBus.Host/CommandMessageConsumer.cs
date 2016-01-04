
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using MassTransit;
using MediatR;
using System.Threading.Tasks;

namespace HomeRunner.Api.CommandBus.Host
{
    public class CommandMessageConsumer :
		IConsumer<CommandMessage<CreateTaskActivityCommand>>,
        IConsumer<CommandMessage<ClaimTaskActivityCommand>>,
        IConsumer<CommandMessage<UnclaimTaskActivityCommand>>
    {
        private readonly IMediator mediator;

        private readonly IDomainEventMessagePublisher publisher;

        public CommandMessageConsumer() { }

        public CommandMessageConsumer(IMediator mediator, IDomainEventMessagePublisher publisher)
            : this()
        {
            this.mediator = mediator;
            this.publisher = publisher;
        }

		public Task Consume(ConsumeContext<CommandMessage<CreateTaskActivityCommand>> context)
		{
			return ConsumeCommand(context);
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

                Logger.Log.InfoFormat(Logger.CORRELATED_MESSAGE, context.Message.Command.Id, "consuming message");
                ICommandResult events = this.mediator.Send(context.Message.Command);

                Logger.Log.DebugFormat(Logger.CORRELATED_MESSAGE, context.Message.Command.Id, "message consumed");
                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, context.Message.Command.Id, "domain events", events.ToJson());
            });
        }
    }
}
