using System.Collections.Generic;
using System.Linq;

namespace DataContext.Core
{
    public class EntityFrameworkPersonRepository : IPersonRepository
    {
        private readonly CurrentContext context;

        public EntityFrameworkPersonRepository(CurrentContext context)
        {
            this.context = context;
        }

        public List<Person> WithFirstName(string firstname)
        {
            using (var model = new DataContextModelContainer())
            {
                var people = from p in model.People 
                             where p.FirstName == firstname && p.ContextId == context.Id
                             select p;

                return people.ToList();
            }
        }

        public List<Person> WithLastName(string lastname)
        {
            using (var model = new DataContextModelContainer())
            {
                var people = from p in model.People
                             where p.LastName == lastname && p.ContextId == context.Id
                             select p;

                return people.ToList();
            }
        }

        public List<Person> WithName(string name)
        {
            using (var model = new DataContextModelContainer())
            {
                var people = from p in model.People
                             where name == p.FirstName + " " + p.LastName
                             && p.ContextId == context.Id
                             select p;

                return people.ToList();
            }
        }

        public void Save(Person person)
        {
            using (var model = new DataContextModelContainer())
            {
                person.ContextId = context.Id;
                model.People.AddObject(person);
                model.SaveChanges();
            }
        }
    }
}
