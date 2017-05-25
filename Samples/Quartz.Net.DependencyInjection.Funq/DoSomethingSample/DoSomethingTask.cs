using System;
using System.Threading.Tasks;
using Serilog;

namespace Quartz.Net.DependencyInjection.Funq.DoSomethingSample
{
    public class DoSomethingTask : BaseTask
    {
        private readonly ILogger logger;

        public DoSomethingTask(
            ILogger logger)
        {
            this.logger = logger;
        }

        public override Task ExecuteAsync()
        {
            try
            {
                logger.Debug("Invoked DoSomethingTask");
                //    await -> something is beeing done here
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error in DoSomethingTask");
            }
            logger.Debug("Completed DoSomethingTask");

            //remove this when await is implemented
            return Task.CompletedTask;
        }
    }
}