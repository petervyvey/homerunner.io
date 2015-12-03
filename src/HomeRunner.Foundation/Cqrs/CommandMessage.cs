

namespace HomeRunner.Foundation.Cqrs
{
    public sealed class CommandMessage<TCommand>
        : ICommandMessage<TCommand, ICommandResult>
        where TCommand : ICommand<ICommandResult>
    {
        public CommandMessage() { }

        public CommandMessage(TCommand command)
            : this()
        {
            this.Command = command;
        }

        public TCommand Command { get; private set; }
    }
}
