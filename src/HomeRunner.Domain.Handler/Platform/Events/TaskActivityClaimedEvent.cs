
using HomeRunner.Domain.Service.Platform.Commands;
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.Service.Platform.Events
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
