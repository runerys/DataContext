using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContext.IntegrationTests
{
    using DataContext.Core;

    using NUnit.Framework;

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

                entities.AddToPeople(person);
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

                entities.AddToPeople(person);
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

            this.CreateAndBindNewContext();
        }

        private void CreateAndBindNewContext()
        {
            using (var entities = new DataContextModelContainer())
            {
                var currentContext = new Context();
                currentContext.Name = "Testcontext 1234";
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