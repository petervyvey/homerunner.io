
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Events
{
    public class TaskActivityUnclaimedEvent
        : DomainEvent
    {
        public TaskActivityUnclaimedEvent(UnclaimTaskActivityCommand command)
            : base(command.Id)
        {
            this.TaskId = command.TaskId;
        }

        public Guid TaskId { get; set; }
    }
}
