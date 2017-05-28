using System;
using Quartz.Spi;

namespace Quartz.Net.DependencyInjection._3._0
{
   
    /// <summary>
    ///     Resolve Quartz Job and it's dependencies from container
    /// </summary>
    public class ContainerJobFactory : IJobFactory
    {
        private readonly IQuartzContainerAdapter container;

        /// <summary>
        /// Initialises a new instance of the ContainerJobFactory
        /// </summary>
        /// <param name="container"></param>
        public ContainerJobFactory(IQuartzContainerAdapter container)
        {
            this.container = container;
        }

        /// <summary>
        ///     Called by the Quartz Scheduler at the time of the trigger firing
        ///     in order to produce a IJob instance on which to call execute
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (bundle == null) throw new ArgumentNullException(nameof(bundle));
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

            var jobDetail = bundle.JobDetail;
            var newJob = (IJob)container.Resolve(jobDetail.JobType);

            if (newJob == null)
                throw new SchedulerConfigException($"Failed to instantiate Job {jobDetail.Key} of type {jobDetail.JobType}");

            return newJob;
        }

        /// <summary>
        ///     Allows the job factory to destroy/cleanup the job if needed
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                var disposableJob = disposable;
                disposableJob.Dispose();
            }
        }
    }

}
