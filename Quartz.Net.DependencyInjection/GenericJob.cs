using System.Diagnostics;

namespace Quartz.Net.DependencyInjection
{
        
    public class GenericJob<T> : IJob where T: IQuartzTask
    {
        [DebuggerStepThrough]
        public void Execute(IJobExecutionContext context)
        {
            var task = context.Get<T>();
            task.ExecuteAsync().Wait();
        }
    }
}
