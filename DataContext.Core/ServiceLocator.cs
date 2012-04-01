namespace DataContext.Core
{
    using System;
    using System.Reflection;
    using Autofac;

    public class ServiceLocator : IServiceLocator
    {
        private IContainer _container;

        public ServiceLocator()
        {
            SetupContainerWithContext();           
        }

        private void SetupContainerWithContext()
        {
            var containerBuilder = new ContainerBuilder();

            var currentAssembly = Assembly.GetAssembly(this.GetType());

            containerBuilder.RegisterAssemblyTypes(currentAssembly).AsImplementedInterfaces();

            var currentContext = GetCurrentContext();
            containerBuilder.RegisterInstance(currentContext);

            _container = containerBuilder.Build();            
        }

        private CurrentContext GetCurrentContext()
        {
            var realContextId = 0; // or read settings / registry...
            return new CurrentContext(realContextId);            
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void RebindToConstant<T>(T instance) where T : class
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(instance);
            builder.Update(_container);
        }

        public void Rebind<TInterface>(Type type)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(type).As<TInterface>();
            builder.Update(_container);
        }

        public void Register(Type type)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(type);
            builder.Update(_container);
        }
    }
}