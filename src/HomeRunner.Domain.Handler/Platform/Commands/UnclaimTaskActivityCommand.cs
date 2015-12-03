
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.Service.Platform.Commands
{
    public class UnclaimTaskActivityCommand
        : Command
    {
        public UnclaimTaskActivityCommand() { }

        public UnclaimTaskActivityCommand(Guid taskId)
            : this()
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
