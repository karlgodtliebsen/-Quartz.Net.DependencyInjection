namespace Quartz.Net.DependencyInjection
{
    public interface IQuartzContainerAdapter
    {
        void Register<T> ();
        T Resolve<T>();
    }
}