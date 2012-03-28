namespace DataContext.Core
{
    using System.Collections.Generic;

    public interface IPersonRepository
    {
        List<Person> WithFirstName(string firstname);

        void Save(Person person);

    }
}