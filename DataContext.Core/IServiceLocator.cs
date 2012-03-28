namespace DataContext.Core
{
    public interface IServiceLocator
    {
        T Resolve<T>();
        void RebindToConstant<T>(T instance) where T : class;
    }
}
