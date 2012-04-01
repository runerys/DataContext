using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataContext.Core
{
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
                var people = from p in model.People.In(model.Context)
                             where p.FirstName == firstname
                             select p;

                return people.ToList();
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

    public static class Extensions
    {                      
        public static IQueryable<T> In<T>(this IQueryable<T> query, int contextId)
        {
            // Convention: Filter on ContexId if property is present.
            // Don't want to introduce IContextAware interface.

            var contextProperty = typeof(T).GetProperty("ContextId");

            if (contextProperty == null)
                return query;

            var expression = In<T>(contextId);

            return query.Where(expression);
        }

        public static Expression<Func<TEntity, bool>> In<TEntity>(int contextId)
        {
            // Create the lambda: "e => e.ContextId == contextId"

            var entityParameter = Expression.Parameter(typeof(TEntity));
            var contextIdProperty = Expression.Property(entityParameter, "ContextId");

            var contextIdConstant = Expression.Constant(contextId);            
            var comparison = Expression.Equal(contextIdProperty, contextIdConstant);

            return Expression.Lambda<Func<TEntity, bool>>(comparison, new[] { entityParameter });
        }
    }

    public class ModelFactory
    {
        private readonly CurrentContext _currentContext;

        public ModelFactory(CurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        public DataContextModelContainer New()
        {
            return new DataContextModelContainer(_currentContext);
        }
    }
}