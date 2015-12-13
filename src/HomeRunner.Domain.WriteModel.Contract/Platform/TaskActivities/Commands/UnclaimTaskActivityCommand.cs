
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands
{
    public class UnclaimTaskActivityCommand
        : Command
    {
        public UnclaimTaskActivityCommand()
			: base() { }

        public UnclaimTaskActivityCommand(Guid taskId)
            : this()
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
