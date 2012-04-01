namespace DataContext.Core.ContextAware
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Experimenting on how to make the repository less aware about the contextId
    /// </summary>
    public class EntityFrameworkPersonRepositoryWithContextAwareModel : IPersonRepository
    {
        private readonly ModelFactory _modelFactory;

        public EntityFrameworkPersonRepositoryWithContextAwareModel(ModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public List<Person> WithFirstName(string firstname)
        {
            using (var model = _modelFactory.New())
            {
                // First try: Kind of "fluent"
                var people = from p in model.People.In(model.Context)
                             where p.FirstName == firstname
                             select p;

                return people.ToList();
            }
        }

        public List<Person> WithLastName(string lastname)
        {
            using (var model = _modelFactory.New())
            {
                // Second try: A bit backwards
                var people = from p in model.WithContextFilterOn(m => m.People)
                             where p.LastName == lastname
                             select p;

                return people.ToList();
            }
        }

        public List<Person> WithName(string name)
        {
            using (var model = _modelFactory.New())
            {
                // Probably the better alternative this far
                var people = from p in model.People.InContext()
                             where name == p.FirstName + " " + p.LastName
                             select p;

                return people.ToList();
            }
        }

        public List<Person> WithName2(string name)
        {
            using (var model = _modelFactory.New())
            {
                var people = from p in model.People
                             where name == p.FirstName + " " + p.LastName
                             select p;

                // Experimenting - not that happy about this one
                return people.ToListInContext(model.ContextId);
            }
        }       

        public void Save(Person person)
        {
            using (var model = _modelFactory.New())
            {
                model.People.AddObject(person);
                model.SaveChanges();
            }
        }
    }
}