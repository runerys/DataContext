namespace DataContext.Core
{
    using System;

    public interface IServiceLocator
    {
        T Resolve<T>();
        void RebindToConstant<T>(T instance) where T : class;

        void Rebind<TInterface>(Type type);

        void Register(Type type);
    }
}
