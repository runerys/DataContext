namespace DataContext.Core
{
    using System.Collections.Generic;

    public interface IPersonRepository
    {
        List<Person> WithFirstName(string firstname);

        List<Person> WithLastName(string lastname);

        List<Person> WithName(string name);

        void Save(Person person);
    }
}