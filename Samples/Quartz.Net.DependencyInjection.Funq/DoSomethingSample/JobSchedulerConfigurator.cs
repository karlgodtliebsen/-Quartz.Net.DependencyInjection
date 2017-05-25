using System;
using System.Collections;
using Funq;

namespace Quartz.Net.DependencyInjection.Funq.DoSomethingSample
{

    public static class JobSchedulerConfigurator
    {
        public static Container ConfigureScheduledJobs(this Container container, IScheduler scheduler)
        {
            IQuartzContainerAdapter funqContainer = new QuartzToFunqContainerAdapter(container);
            IDictionary map = JobDependecyExtensions.CreateMap(funqContainer);
            ConfigureDoSomethingJob(funqContainer, scheduler, map);
            return container;
        }


        private static void ConfigureDoSomethingJob(IQuartzContainerAdapter container, IScheduler scheduler, IDictionary map)
        {
            IJobDetail job = JobBuilder.Create<GenericJob<DoSomethingTask>>()
                .SetJobData(new JobDataMap(map))
                .WithIdentity("DoSomethingJob", "DoSomethingJobGroup")
                .WithTask<DoSomethingTask>(container)
                .Build();

            var interval = (int)TimeSpan.FromSeconds(1).TotalSeconds;

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("DoSomethingJobTrigger", "DoSomethingJobGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(interval)
                        .RepeatForever()
                )
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
