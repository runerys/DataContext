namespace DataContext.Core
{
    using System.Reflection;
    using Autofac;

    public class AutofacServiceLocator : IServiceLocator
    {
        private IContainer _container;

        public AutofacServiceLocator()
        {
            SetupContainer();           
        }

        private void SetupContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(Assembly.GetAssembly(this.GetType())).AsImplementedInterfaces();
            _container = containerBuilder.Build();

            ApplySpecialBindings();
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