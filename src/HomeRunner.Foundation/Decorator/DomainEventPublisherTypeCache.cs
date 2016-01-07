
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Logging;
using System;
using System.Collections.Concurrent;

namespace HomeRunner.Foundation.Decorator
{
    internal class DomainEventPublisherTypeCache
    {
        private static readonly ConcurrentDictionary<Type, Type> MESSAGE_TYPES = new ConcurrentDictionary<Type, Type>();

        internal static Type Get(Type requestedType, Guid correlationId)
        {
            return DomainEventPublisherTypeCache.Get(requestedType, correlationId.ToString());
        }

        internal static Type Get(Type requestedType, string correlationId = "__UNSPECIFIED__")
        {
            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, correlationId, "find domain event type message type for ", requestedType);
            Type messageType;
            if (!DomainEventPublisherTypeCache.MESSAGE_TYPES.TryGetValue(requestedType, out messageType))
            {
                Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, correlationId, "domain event message type not found", requestedType);

                Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, correlationId, "create domain event message type", requestedType);
                messageType = DomainEventPublisherTypeCache.MESSAGE_TYPES[requestedType] = typeof(DomainEventMessage<>).MakeGenericType(requestedType);

                Logger.Log.WarnFormat(Logger.CORRELATED_CONTENT, correlationId, "domain event message type created", messageType.FullName);
            }
            Logger.Log.DebugFormat(Logger.CORRELATED_CONTENT, correlationId, "domain event message type found", messageType.Name);

            return messageType;
        }
    }
}
