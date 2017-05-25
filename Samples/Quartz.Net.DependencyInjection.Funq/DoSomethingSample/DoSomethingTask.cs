using System;
using System.Threading.Tasks;
using Serilog;

namespace Quartz.Net.DependencyInjection.Funq.DoSomethingSample
{
    public class DoSomethingTask : BaseTask
    {
        private readonly IMyService service;
        private readonly ILogger logger;

        public DoSomethingTask(IMyService service,ILogger logger)
        {
            this.service = service;
            this.logger = logger;
        }

        public override async Task ExecuteAsync()
        {
            try
            {
                logger.Debug("Invoked DoSomethingTask");
                await service.DoItAsync();
                logger.Debug("Completed DoSomethingTask");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error in DoSomethingTask");
            }
        }
    }
}