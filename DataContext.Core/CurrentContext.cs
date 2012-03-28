namespace DataContext.Core
{
    public class CurrentContext
    {
        public CurrentContext(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}