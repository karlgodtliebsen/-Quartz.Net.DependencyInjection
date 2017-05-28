using System;
using System.Collections;
using System.Threading.Tasks;
using Funq;
using Quartz.Net.DependencyInjection._3._0;
using Quartz.Spi;

namespace Quartz.Net.DependencyInjection.Funq._3._0.DoSomethingSample
{

    public static class JobSchedulerConfigurator
    {
        public static async Task<Container> AddScheduledJobs(this Container container, Task<IScheduler> scheduler)
        {
            Configure(container, await scheduler);
            await ConfigureDoSomethingJob(await scheduler);
            return container;
        }

        private static void Configure(Container container, IScheduler scheduler)
        {
            IQuartzContainerAdapter funqContainer = new QuartzToFunqContainerAdapter(container);    //<--  DI Container wrapper. Can be any
            scheduler.JobFactory = new ContainerJobFactory(funqContainer);
            container.Register<IScheduler>(scheduler);
        }

        private static async Task ConfigureDoSomethingJob(IScheduler scheduler)
        {
            var interval = TimeSpan.FromMilliseconds(10);

            IJobDetail job = JobBuilder.Create<DoSomethingJob>()
                .WithIdentity("DoSomethingJob", "DoSomethingJobGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("DoSomethingJobTrigger", "DoSomethingJobGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                        .WithInterval(interval)
                        .RepeatForever()
                )
                .Build();
            await scheduler.ScheduleJob(job, trigger);

           
        }
    }
}
