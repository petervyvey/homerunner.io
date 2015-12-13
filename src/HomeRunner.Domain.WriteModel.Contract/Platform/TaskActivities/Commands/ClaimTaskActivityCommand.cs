
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands
{
    public class ClaimTaskActivityCommand
        : Command
    {
        public ClaimTaskActivityCommand()
			: base() { }

        public ClaimTaskActivityCommand(Guid taskId)
            : this()
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
