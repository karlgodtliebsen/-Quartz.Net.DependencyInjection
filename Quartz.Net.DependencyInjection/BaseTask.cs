using System.Threading.Tasks;

namespace Quartz.Net.DependencyInjection
{
    public abstract class BaseTask
    {
        public abstract Task ExecuteAsync();
    }
}