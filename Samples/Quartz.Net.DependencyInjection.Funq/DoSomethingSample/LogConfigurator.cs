using Funq;
using Serilog;
using Serilog.Events;

namespace Quartz.Net.DependencyInjection.Funq.DoSomethingSample
{
    public static class LogConfigurator
    {
        public static Container AddLogger(this Container container, ILogger logger)
        {
            container.Register<ILogger>(l => logger).ReusedWithin(ReuseScope.Container);
            Log.Logger = logger;
            return container;
        }

        public static ILogger Create()
        {
             var logConfiguration = new LoggerConfiguration()
                 .WriteTo.ColoredConsole()
                ;
            return logConfiguration.CreateLogger();
        }
    }
}