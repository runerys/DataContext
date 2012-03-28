using System;
using System.Linq;

namespace DataContext.IntegrationTests
{
    using DataContext.Core;

    using NUnit.Framework;

    [TestFixture]
    public class WithBaseClassTests : SetUpAndTearDownWithNewContextForEachTest
    {
        [Test]
        public void GivenPerson_WhenSearching_IsAbleToFindHim()
        {
            // Arrange
            using (var entities = new DataContextModelContainer())
            {
                var person = new Person { FirstName = "Rune", LastName = "Rystad" };
                person.ContextId = CurrentContextId;

                entities.People.AddObject(person);
                entities.SaveChanges();
            }

            // Act
            var search = new DataContextModelContainer();

            var people = from p in search.People
                         where p.FirstName == "Rune"
                         && p.ContextId == CurrentContextId
                         select p;

            var foundPerson = people.First();
            var countOfHits = people.Count();

            // Assert
            Assert.AreEqual(1, countOfHits);
            Assert.AreEqual("Rystad", foundPerson.LastName);
        }

        [Test]
        public void GivenAnotherPerson_WhenSearching_IsAbleToFindHim()
        {
            // Arrange
            using (var entities = new DataContextModelContainer())
            {
                var person = new Person { FirstName = "Rune", LastName = "Rudberg" };
                person.ContextId = CurrentContextId;

                entities.People.AddObject(person);
                entities.SaveChanges();
            }

            // Act
            using (var search = new DataContextModelContainer())
            {
                var people = from p in search.People
                             where p.FirstName == "Rune" && p.ContextId == CurrentContextId
                             select p;

                var foundPerson = people.First();
                var countOfHits = people.Count();

                // Assert
                Assert.AreEqual(1, countOfHits);
                Assert.AreEqual("Rudberg", foundPerson.LastName);
            }
           
        }
    }

    public class SetUpAndTearDownWithNewContextForEachTest
    {
        protected int CurrentContextId { get; private set; }

        [SetUp]
        public void Setup()
        {
            using (var entities = new DataContextModelContainer())
            {
                var currentContext = new Context();
                currentContext.Name = "Testcontext 1234";
                currentContext.IsTest = true;
                currentContext.DateCreated = DateTime.Now;

                entities.Contexts.AddObject(currentContext);

                entities.SaveChanges();

                CurrentContextId = currentContext.Id;
            }
        }

        [TearDown]
        public void TearDown()
        {
            
        }
    }
}
