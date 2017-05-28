using System.Threading;
using System.Threading.Tasks;

namespace Quartz.Net.DependencyInjection.Funq._3._0.DoSomethingSample
{
    public class MyService : IMyService
    {
        private readonly ManualResetEvent manualResetEvent;

        public MyService(ManualResetEvent manualResetEvent)
        {
            this.manualResetEvent = manualResetEvent;
        }

        public bool IsHit { get; set; }

        public Task DoItAsync()
        {

            IsHit = true;

            manualResetEvent.Set();

            //do something smart
            return Task.CompletedTask;
        }
    }
}