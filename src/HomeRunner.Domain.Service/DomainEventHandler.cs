
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Logging;
using MassTransit;

namespace HomeRunner.Domain.Service
{
    //public class DomainEventHandler
    //    : 
    //    IEventHandler<IDomainEventMessage<IDomainEvent>>
    //    ,
    //    IEventHandler<IDomainEventMessage<TaskActivityClaimedEvent>>
    //{
    //    private readonly IBus bus;

    //    public DomainEventHandler(IBus bus)
    //    {
    //        Logger.Log.Info(string.Format("Creating event handler: {0}", this.GetType().AssemblyQualifiedName));
    //        this.bus = bus;

    //        Logger.Log.Debug(string.Format("Created event handler: {0}", this.GetType().AssemblyQualifiedName));
    //    }

    //    public void Handle(IDomainEventMessage<IDomainEvent> message)
    //    {
    //        var _notification = message.DomainEvent.ToJson();

    //        Logger.Log.Info(string.Format("Publishing to event bus:\r\n{0}", _notification));
    //        this.bus.Publish(message);

    //        Logger.Log.Debug(string.Format("Published to event bus:\r\n{0}", _notification));
    //    }

    //    public void Handle(IDomainEventMessage<TaskActivityClaimedEvent> message)
    //    {
    //        var _notification = message.DomainEvent.ToJson();

    //        Logger.Log.Info(string.Format("Publishing to event bus:\r\n{0}", _notification));
    //        this.bus.Publish(message);

    //        Logger.Log.Debug(string.Format("Published to event bus:\r\n{0}", _notification));
    //    }
    //}
}
