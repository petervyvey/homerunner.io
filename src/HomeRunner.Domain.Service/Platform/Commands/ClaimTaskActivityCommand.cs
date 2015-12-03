
using HomeRunner.Foundation.Cqrs;
using System;
using System.Linq.Expressions;

namespace HomeRunner.Domain.Service.Platform.Commands
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
