using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Quartz.Net.DependencyInjection
{
    public static class JobDependecyExtensions
    {
        private const string Key = "QuartzContainer";
        [DebuggerStepThrough]
        public static IDictionary CreateMap(IQuartzContainerAdapter containerAdapter)
        {
            IDictionary map = new Dictionary<string, IQuartzContainerAdapter>() { { Key, containerAdapter } };
            return map;
        }
        [DebuggerStepThrough]
        public static JobBuilder WithTask<T>(this JobBuilder builder, IQuartzContainerAdapter containerAdapter)
        {
            containerAdapter.Register<T>();
            return builder;
        }

        [DebuggerStepThrough]
        public static T Get<T>(this IJobExecutionContext context)
        {
            IDictionary map = context.MergedJobDataMap;
            IQuartzContainerAdapter containerAdapter = (IQuartzContainerAdapter)map[Key];
            return containerAdapter.Resolve<T>();
        }

    }
}