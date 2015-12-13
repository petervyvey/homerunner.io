
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Events
{
    public class TaskActivityClaimedEvent
        : DomainEvent
    {
        public TaskActivityClaimedEvent(ClaimTaskActivityCommand command)
            : base(command.Id)
        {
            this.TaskId = command.TaskId;
        }

        public Guid TaskId { get; set; }
    }
}
