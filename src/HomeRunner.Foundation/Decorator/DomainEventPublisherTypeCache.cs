
using System;
using System.Collections.Concurrent;

namespace HomeRunner.Foundation.Decorator
{
    public class DomainEventPublisherTypeCache
    {
        internal static readonly ConcurrentDictionary<Type, Type> MESSAGE_TYPES = new ConcurrentDictionary<Type, Type>();
    }
}
