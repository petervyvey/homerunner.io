
using Autofac;
using MassTransit;

namespace HomeRunner.Consumer.Host
{
    internal sealed class MassTransitConfig
    {
        internal static IBusControl Configure(IContainer container)
        {
            IBusControl busControl = container.Resolve<IBusControl>();
            return busControl;
        }
    }
}
