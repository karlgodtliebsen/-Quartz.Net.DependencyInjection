using System;
using System.Collections;
using Funq;

namespace Quartz.Net.DependencyInjection.Funq.DoSomethingSample
{

    public static class JobSchedulerConfigurator
    {
        public static Container AddScheduledJobs(this Container container, IScheduler scheduler)
        {
            IQuartzContainerAdapter funqContainer = new QuartzToFunqContainerAdapter(container);    //<--  DI Container wrapper. Can be any
            IDictionary map = JobDependecyExtensions.CreateMap(funqContainer);                      //<--  stores the container in context

            ConfigureDoSomethingJob(funqContainer, scheduler, map);

            return container;
        }


        private static void ConfigureDoSomethingJob(IQuartzContainerAdapter container, IScheduler scheduler, IDictionary map)
        {
            scheduler.StartDelayed(TimeSpan.FromMilliseconds(5));
            var interval = TimeSpan.FromMilliseconds(10);

            IJobDetail job = JobBuilder.Create<GenericJob<DoSomethingTask>>()   //<--  notice use of GenericJob With a Task
                .SetJobData(new JobDataMap(map))
                .WithIdentity("DoSomethingJob", "DoSomethingJobGroup")
                .WithTask<DoSomethingTask>(container)                           //<--  notice configuration of a Task
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("DoSomethingJobTrigger", "DoSomethingJobGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                        .WithInterval(interval)
                        .RepeatForever()
                )
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
