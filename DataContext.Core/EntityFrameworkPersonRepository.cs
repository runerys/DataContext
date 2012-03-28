using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
