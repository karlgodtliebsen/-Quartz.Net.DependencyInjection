using System;
using System.Threading;
using Funq;
using NUnit.Framework;
using Quartz.Impl;
using Serilog;

namespace Quartz.Net.DependencyInjection.Funq.DoSomethingSample
{
    [TestFixture]
    public class TestOfDoSomething
    {
        private readonly IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private readonly Container container = new Container();
        private readonly ILogger logger = LogConfigurator.Create();


        [Test]
        public void MyServiceShouldBeInvoked()
        {
            var theService = new MyService(manualResetEvent);

            logger.Debug("Wiring up the DI Container and the Scheduler");
            container
                 .AddLogger(logger)
                 .AddScheduledJobs(scheduler)
                 .Register<IMyService>(theService);

            logger.Debug("Waiting");
            manualResetEvent.WaitOne(TimeSpan.FromSeconds(1));

            scheduler.Shutdown();

            Assert.IsTrue(theService.IsHit);

            logger.Debug("Done");

        }


    }
}
