using System;

namespace Quartz.Net.DependencyInjection._3._0
{
    public interface IQuartzContainerAdapter
    {
        void Register<T> ();

        object Resolve(Type t);
        
    }
}