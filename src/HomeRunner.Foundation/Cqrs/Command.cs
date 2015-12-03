
using System;

namespace HomeRunner.Foundation.Cqrs
{
    public class Command
        : ICommand<ICommandResult>
    {
        public Command()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
