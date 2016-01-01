
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
            string correlationId = request is ICommand<TRequest> ? (request as ICommand<TRequest>).Id.ToString() : "__UNSPECIFIED__";

            try
            {
                Logger.Log.InfoFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "handling", _request);
                TRequestResult events = this.handler.Handle(request);

                Logger.Log.DebugFormat(Logger.CORRELATED_LONG_CONTENT, correlationId, "handled", _request);

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
