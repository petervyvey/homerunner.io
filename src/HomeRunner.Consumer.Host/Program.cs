
using Autofac;
using HomeRunner.Foundation.Logging;
using log4net.Config;
using MassTransit;
using System;


namespace HomeRunner.Consumer.Host
{
    class Program
    {
        private static IContainer container;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            Logger.Log.Info("Building AutoFac container");
            container = AutofacConfig.BuildContainer();
            Logger.Log.Debug("Build AutoFac container");

            Logger.Log.Info("Starting MassTransit BusControl");
            IBusControl control = MassTransitConfig.Configure(container);
            control.Start();
            Logger.Log.Info("Started MassTransit BusControl");

            Logger.Log.Info("Instantiating consumers");
            ConnectHandle handle = control.ConnectConsumer<CommandMessageConsumer>();
            Logger.Log.Info("Instantiated consumers");

            Console.ReadKey();

            handle.Disconnect();
            control.Stop();
        }
    }
}
