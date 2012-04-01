using System;
using System.Linq;
using DataContext.Core;
using NUnit.Framework;

namespace DataContext.IntegrationTests
{
    using DataContext.Core.ContextAware;

    [TestFixture]
    public class PersonRepositoryWithContextAwareModelTests : SetupAndTearDown<ServiceLocator>
    {
        [SetUp]
        public void Setup()
        {
            ServiceLocator.Rebind<IPersonRepository>(typeof(EntityFrameworkPersonRepositoryWithContextAwareModel));
            ServiceLocator.Register(typeof(ModelFactory));
        }

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

        [Test]
        public void GivenEmptyContext_WhenCreatingANewPersonAndSearchingByLastname_IsAbleToFindHim()
        {
            var persons = ServiceLocator.Resolve<IPersonRepository>();

            var person = new Person();
            person.FirstName = "Rune";
            person.LastName = "Rystad";

            persons.Save(person);

            var people = persons.WithLastName("Rystad");

            Assert.AreEqual(1, people.Count());
            Assert.AreEqual("Rystad", people.First().LastName);
        }

        [Test]
        public void GivenEmptyContext_WhenCreatingANewPersonAndSearchingByFullname_IsAbleToFindHim()
        {
            var persons = ServiceLocator.Resolve<IPersonRepository>();

            var person = new Person();
            person.FirstName = "Rune";
            person.LastName = "Rystad";

            persons.Save(person);

            var people = persons.WithName("Rune Rystad");

            Assert.AreEqual(1, people.Count());
            Assert.AreEqual("Rystad", people.First().LastName);
        }
    }

   
}