
using System;

namespace HomeRunner.Foundation.Web
{
    public sealed class CommandResponse
    {
        public CommandResponse(Guid commandId)
        {
            this.CommandId = commandId;
        }

        public Guid CommandId { get; private set; }
    }
}