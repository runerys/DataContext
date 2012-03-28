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
            containerBuilder.RegisterAssemblyTypes(Assembly.GetAssembly(this.GetType())).AsImplementedInterfaces();
            _container = containerBuilder.Build();

            BindContext();

            ApplySpecialBindings();
        }

        private void BindContext()
        {
            // Real context = 0
            var currentContext = new CurrentContext(0); // or read settings / registry...

            RebindToConstant(currentContext);
        }

        protected virtual void ApplySpecialBindings() { }

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