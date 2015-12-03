
using HomeRunner.Foundation.Extension;
using MediatR;
using System;

namespace HomeRunner.Foundation.Logging

{
    public class LoggingDecorator<TCommand, TCommandResult>
        : IRequestHandler<TCommand, TCommandResult>
        where TCommand : IRequest<TCommandResult>
        where TCommandResult: class 
    {
        private readonly IRequestHandler<TCommand, TCommandResult> handler;

        public LoggingDecorator(IRequestHandler<TCommand, TCommandResult> handler)
        {
            this.handler = handler;
        }

        public TCommandResult Handle(TCommand command)
        {
            try
            {
                string _command = command.ToJson();

                LogInstance.Log.Info(string.Format("Handling command:\r\n{0}", _command));
                TCommandResult events = this.handler.Handle(command);

                LogInstance.Log.Debug(string.Format("Command handled:\r\n{0}", _command));

                return events;
            }
            catch (Exception ex)
            {
                LogInstance.Log.Error(ex.Message);
                throw;
            }
        }
    }
}
