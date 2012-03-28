namespace DataContext.Core
{
    using System.Reflection;
    using Autofac;

    public class ServiceLocator : IServiceLocator
    {
        private IContainer _container;

        public ServiceLocator()
        {
            SetupContainer();           
        }

        private void SetupContainer()
        {
            var containerBuilder = new ContainerBuilder();

            var currentAssembly = Assembly.GetAssembly(this.GetType());

            containerBuilder.RegisterAssemblyTypes(currentAssembly).AsImplementedInterfaces();

            _container = containerBuilder.Build();

            BindContext();

        }

        protected virtual void BindContext()
        {
            var realContextId = 0; // or read settings / registry...
            var currentContext = new CurrentContext(realContextId); 

            RebindToConstant(currentContext);
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
    }
}