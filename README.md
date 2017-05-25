# -Quartz.Net.DependencyInjection
 
 By using a simple extension/wrapping a Dependency Injection Container is added to Quartz.Net without using the IJobFactory, and with support for Async.
 
 
  Configuration sample:
  
  
  public static class JobSchedulerConfigurator
    {
        public static Container AddScheduledJobs(this Container container, IScheduler scheduler)
        {
            //a specific implementation for funq Container
            IQuartzContainerAdapter funqContainer = new QuartzToFunqContainerAdapter(container);
           
            //store the container adapter in context
            IDictionary map = JobDependecyExtensions.CreateMap(funqContainer);
            //Do some Job Configuration
            ConfigureDoSomethingJob(funqContainer, scheduler, map);
            //support fluent configuration
            return container;
        }


        private static void ConfigureDoSomethingJob(IQuartzContainerAdapter container, IScheduler scheduler, IDictionary map)
        {
            //Use a genereic job wrapper that handles a Task
            IJobDetail job = JobBuilder.Create<GenericJob<DoSomethingTask>>()
                .SetJobData(new JobDataMap(map))
                .WithIdentity("DoSomethingJob", "DoSomethingJobGroup")
                .WithTask<DoSomethingTask>(container)//Specify the Task to invoke
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
    
    
    
    A sample Container Adapter (Funq Container), can be any DI framework
    
    
    public class QuartzToFunqContainerAdapter:IQuartzContainerAdapter
    {
        private readonly Container container;
        [DebuggerStepThrough]
        public QuartzToFunqContainerAdapter(Container container)
        {
            this.container = container;
        }
        [DebuggerStepThrough]
        public void Register<T>()
        {
            container.RegisterAutoWired<T>().ReusedWithin(ReuseScope.None);
        }
        [DebuggerStepThrough]
        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
    
A Task (Is used instead of a specific Quartz.Net Job. The Generic Quartz.Net Job invokes the async ExecuteAsync methods and Waits (similar to a ConsoleHost waiting for a Async Task:
   
   public class DoSomethingTask : BaseTask //old style base class - could be interface
    {
        private readonly ILogger logger;//SeriLog sample
        private readonly IMyService service;
        public DoSomethingTask(/*Inject services like unitofwork, dbcontext domain service or whatever*/  
                      IMyService service,
                        ILogger logger)
        {
            this.logger = logger;
            this.service=service;
        }

        public override async Task ExecuteAsync()
        {
            try
            {
                logger.Debug("Invoked DoSomethingTask");
                 await service.AMethodAsync();//  
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error in DoSomethingTask");
            }
            logger.Debug("Completed DoSomethingTask");
        }
    }
    
    
