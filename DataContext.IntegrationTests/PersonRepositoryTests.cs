using System;
using System.Linq;
using DataContext.Core;
using NUnit.Framework;

namespace DataContext.IntegrationTests
{
    [TestFixture]
    public class PersonRepositoryTests : SetupAndTearDown<ServiceLocator>
    {
        [Test]
        public void GivenPerson_WhenSearching_IsAbleToFindHim()
        {
            // Arrange
            using (var entities = new DataContextModelContainer())
            {
                var person = new Person { FirstName = "Rune", LastName = "Rystad" };
                person.ContextId = CurrentContext.Id;

                entities.People.AddObject(person);
                entities.SaveChanges();
            }

            // Act
            var persons = ServiceLocator.Resolve<IPersonRepository>();
            var people = persons.WithFirstName("Rune");
           
            // Assert
            Assert.AreEqual(1, people.Count());
            Assert.AreEqual("Rystad", people.First().LastName);
        }

        [Test]
        public void GivenAnotherPerson_WhenSearching_IsAbleToFindHim()
        {
            // Arrange
            using (var entities = new DataContextModelContainer())
            {
                var person = new Person { FirstName = "Rune", LastName = "Rudberg" };
                person.ContextId = CurrentContext.Id;

                entities.People.AddObject(person);
                entities.SaveChanges();
            }

            // Act
            var persons = ServiceLocator.Resolve<IPersonRepository>();
            var people = persons.WithFirstName("Rune");

            // Assert
            Assert.AreEqual(1, people.Count());
            Assert.AreEqual("Rudberg", people.First().LastName);
        }

        [Test]
        public void GivenEmptyContext_WhenCreatingANewPersonAndSearching_IsAbleToFindHim()
        {
            var persons = ServiceLocator.Resolve<IPersonRepository>();

            var person = new Person();
            person.FirstName = "Rune";
            person.LastName = "Rystad";

            persons.Save(person);

            var people = persons.WithFirstName("Rune");

            Assert.AreEqual(1, people.Count());
            Assert.AreEqual("Rystad", people.First().LastName);
        }
    }

    public class SetupAndTearDown<TServiceLocator>
        where TServiceLocator : IServiceLocator, new()
    {
        protected IServiceLocator ServiceLocator { get; private set; }

        protected CurrentContext CurrentContext { get; private set; }       

        [SetUp]
        public void Setup()
        {
            ServiceLocator = new TServiceLocator();

            this.CreateAndRebindContext();
        }

        private void CreateAndRebindContext()
        {
            using (var entities = new DataContextModelContainer())
            {
                var currentContext = new Context();
                currentContext.Name = string.Format("Testcontext {0:u}", DateTime.UtcNow);
                currentContext.IsTest = true;
                currentContext.DateCreated = DateTime.Now;

                entities.Contexts.AddObject(currentContext);

                entities.SaveChanges();

                var context = new CurrentContext(currentContext.Id);

                CurrentContext = context;
                ServiceLocator.RebindToConstant(context);
            }
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}