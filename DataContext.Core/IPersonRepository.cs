namespace DataContext.Core
{
    using System.Collections.Generic;

    public interface IPersonRepository
    {
        List<Person> GetByFirstName(string firstname);

        void Save(Person person);

    }
}