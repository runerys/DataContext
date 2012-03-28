using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContext.IntegrationTests
{
    using DataContext.Core;

    using NUnit.Framework;

    [TestFixture]
    public class FirstTests
    {
        [Test]
        public void GivenPerson_WhenSearching_IsAbleToFindHim()
        {
            int contextId;

            // Arrange
            using (var entities = new DataContextModelContainer())
            {
                var currentContext = entities.Contexts.CreateObject();
                currentContext.Name = "Testcontext 1234";
                currentContext.IsTest = true;
                currentContext.DateCreated = DateTime.Now;

                entities.AddToContexts(currentContext);

                var person = new Person { FirstName = "Rune", LastName = "Rystad" };
                person.ContextId = currentContext.Id;

                entities.AddToPeople(person);
                entities.SaveChanges();

                contextId = currentContext.Id;
            }

            // Act
            using (var search = new DataContextModelContainer())
            {
                var people = from p in search.People 
                             where p.FirstName == "Rune" && p.ContextId == contextId 
                             select p;

                // Assert
                Assert.AreEqual(1, people.Count());
                Assert.AreEqual("Rystad", people.First().LastName);
            }
        }

        [Test]
        public void GivenAnotherPerson_WhenSearching_IsAbleToFindHim()
        {
            int contextId;

            // Arrange
            using (var entities = new DataContextModelContainer())
            {
                var currentContext = entities.Contexts.CreateObject();
                currentContext.Name = "Testcontext 1234";
                currentContext.IsTest = true;
                currentContext.DateCreated = DateTime.Now;

                entities.AddToContexts(currentContext);

                var person = new Person { FirstName = "Rune", LastName = "Rudberg" };
                person.ContextId = currentContext.Id;

                entities.AddToPeople(person);
                entities.SaveChanges();

                contextId = currentContext.Id;
            }

            // Act
            using (var search = new DataContextModelContainer())
            {
                var people = from p in search.People 
                             where p.FirstName == "Rune" && p.ContextId == contextId 
                             select p;

                // Assert
                Assert.AreEqual(1, people.Count());
                Assert.AreEqual("Rudberg", people.First().LastName);
            }
        }
    }
}