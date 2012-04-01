namespace DataContext.Core.ContextAware
{
    public class ModelFactory
    {
        private readonly CurrentContext _currentContext;

        public ModelFactory(CurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        public DataContextModelContainer New()
        {
            return new DataContextModelContainer(_currentContext);
        }
    }
}