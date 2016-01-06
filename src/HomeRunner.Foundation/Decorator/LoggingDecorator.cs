
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Foundation.Infrastructure.Logging;
using MediatR;
using System;

namespace HomeRunner.Foundation.Decorator
{
    public partial class LoggingDecorator<TRequest, TRequestResult>
        : IRequestHandler<TRequest, TRequestResult>
        where TRequest : IRequest<TRequestResult>
        where TRequestResult : class
    {
        private readonly IRequestHandler<TRequest, TRequestResult> handler;

        public LoggingDecorator(IRequestHandler<TRequest, TRequestResult> handler)
        {
            this.handler = handler;
        }

        public TRequestResult Handle(TRequest request)
        {
            string _request = request.ToJson();

            string correlationId = request is ICommand ? (request as ICommand).Id.ToString() : "__UNSPECIFIED__";

            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "pipeline -> ", this.GetType().Name);
            Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "received command", _request);

            try
            {
                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "logging request", _request);
                TRequestResult events = this.handler.Handle(request);

                Logger.Log.DebugFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "request logged", _request);

                return events;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw;
            }
        }
    }
}
