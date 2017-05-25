using System.Diagnostics;
using Funq;

namespace Quartz.Net.DependencyInjection.Funq
{
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
}