
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Commands
{
    public class ClaimTaskActivityCommand
        : Command
    {
        public ClaimTaskActivityCommand() { }

        public ClaimTaskActivityCommand(Guid taskId)
            : this()
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
