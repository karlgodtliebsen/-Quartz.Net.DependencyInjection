using Funq;
using Serilog;

namespace Quartz.Net.DependencyInjection.Funq._3._0.DoSomethingSample
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