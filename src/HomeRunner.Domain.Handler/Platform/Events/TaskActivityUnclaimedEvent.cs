
using HomeRunner.Domain.Service.Platform.Commands;
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.Service.Platform.Events
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
