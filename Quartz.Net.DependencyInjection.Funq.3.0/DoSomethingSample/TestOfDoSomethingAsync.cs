using System;
using System.Threading;
using System.Threading.Tasks;
using Funq;
using NUnit.Framework;
using Quartz.Impl;
using Serilog;

namespace Quartz.Net.DependencyInjection.Funq._3._0.DoSomethingSample
{
    [TestFixture]
    public class TestOfDoSomethingAsync
    {
        private readonly Task<IScheduler> scheduler = StdSchedulerFactory.GetDefaultScheduler();
        private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private readonly Container container = new Container();
        private readonly ILogger logger = LogConfigurator.Create();


        [Test]
        public async Task MyServiceShouldBeInvoked()
        {
            var theService = new MyService(manualResetEvent);

            logger.Debug("Wiring up the DI Container and the Scheduler");

            container.Register<IMyService>(theService);
            container.RegisterAutoWired<DoSomethingJob>();

            await container
                .AddLogger(logger)
                .AddScheduledJobs(scheduler);

            await (await scheduler).StartDelayed(TimeSpan.FromMilliseconds(5));


            logger.Debug("Waiting");
            manualResetEvent.WaitOne(TimeSpan.FromSeconds(1));

            await (await scheduler).Shutdown(true);
            
            Assert.IsTrue(theService.IsHit);

            logger.Debug("Done");

        }


    }
}
