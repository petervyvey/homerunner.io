
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Domain.WriteModel.Commands;
using HomeRunner.Foundation.Cqrs;
using MediatR;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities
{
    public class TaskActivityService :
            ICommandHandler<ClaimTaskActivityCommand>,
            ICommandHandler<UnclaimTaskActivityCommand>
    {
        private readonly IMediator mediator;

        public TaskActivityService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ICommandResult Handle(ClaimTaskActivityCommand command)
        {
            var existing = this.mediator.Send(new TaskActivityQuery(command.TaskId));

            TaskActivity entity = new TaskActivity(existing);
            IDomainEvent domainEvent = entity.Apply(command);

            return new CommandResult(command.Id, domainEvent);
        }

        public ICommandResult Handle(UnclaimTaskActivityCommand command)
        {
            var existing = this.mediator.Send(new TaskActivityQuery(command.TaskId));

            TaskActivity entity = new TaskActivity(existing);
            IDomainEvent domainEvent = entity.Apply(command);

            return new CommandResult(command.Id, domainEvent);
        }
    }
}
