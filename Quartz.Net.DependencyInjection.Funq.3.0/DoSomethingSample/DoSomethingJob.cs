using System;
using System.Threading.Tasks;
using Serilog;

namespace Quartz.Net.DependencyInjection.Funq._3._0.DoSomethingSample
{
    public class DoSomethingJob : IJob
    {
        private readonly IMyService service;
        private readonly ILogger logger;

        public DoSomethingJob(IMyService service,ILogger logger)
        {
            this.service = service;
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Debug("Invoked DoSomethingJob");
                await service.DoItAsync();
                logger.Debug("Completed DoSomethingJob");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error in DoSomethingJob");
            }
        }

      
    }
}