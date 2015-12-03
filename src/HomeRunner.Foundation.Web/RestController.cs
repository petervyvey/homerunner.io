using HomeRunner.Foundation.Cqrs;
using MassTransit;
using MediatR;
using System;
using System.Threading.Tasks;

namespace HomeRunner.Foundation.Web
{
    public abstract class RestController
        : ServiceController
    {
        public IMediator Mediator { get; set; }

        public IBus Bus { get; set; }

        protected TQueryResult ExecuteQuery<TQueryResult>(IQuery<TQueryResult> query)
            where TQueryResult: class
        {
            TQueryResult instance = this.Mediator.Send(query);

            return instance;
        }

        protected async Task<TCommand> SubmitCommand<TCommand>(TCommand command)
            where TCommand : ICommand<ICommandResult>
        {
            ISendEndpoint endpoint = await this.Bus.GetSendEndpoint(new Uri("rabbitmq://localhost/command/NormalPriority"));
            await endpoint.Send(new CommandMessage<TCommand>(command));

            return command;
        }
    }
}
