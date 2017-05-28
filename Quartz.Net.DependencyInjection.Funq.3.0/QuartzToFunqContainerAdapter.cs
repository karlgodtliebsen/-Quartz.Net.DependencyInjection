using System;
using System.Diagnostics;
using Funq;
using Quartz.Net.DependencyInjection._3._0;

namespace Quartz.Net.DependencyInjection.Funq._3._0
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
        public object  Resolve(Type t)
        {
            return container.TryResolve(t);
        }
    }
}