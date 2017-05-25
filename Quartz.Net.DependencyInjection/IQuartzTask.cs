using System.Threading.Tasks;

namespace Quartz.Net.DependencyInjection
{
    public interface IQuartzTask
    {
        Task ExecuteAsync();
    }
}