namespace DataContext.Core.ContextAware
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ContextAwareExtensions
    {
        private const string CONTEXT_ID = "ContextId";

        public static List<T> ToListInContext<T>(this IQueryable<T> query, int contextId)
        {
            return  query.In(contextId).ToList();
        }

        public static T FirstInContext<T>(this IQueryable<T> query, int contextId)
        {
            return query.In(contextId).First();
        }

        private static readonly Dictionary<Type, bool> TypeHasContextIdPropertyCache = new Dictionary<Type, bool>();

        public static IQueryable<T> In<T>(this IQueryable<T> query, int contextId)
        {
            // Convention: Filter on ContexId if property is present.
            // Don't want to introduce IContextAware interface.

            var entityType = typeof(T);

            if (!TypeHasContextIdPropertyCache.ContainsKey(entityType))
            {
                var contextProperty = entityType.GetProperty(CONTEXT_ID);
                var hasContextIdProperty = contextProperty != null;

                TypeHasContextIdPropertyCache.Add(entityType, hasContextIdProperty);
            }
            
            if (!TypeHasContextIdPropertyCache[entityType])
                return query;

            var expression = In<T>(contextId);

            return query.Where(expression);
        }

        public static Expression<Func<TEntity, bool>> In<TEntity>(int contextId)
        {
            // Create the lambda: "e => e.ContextId == contextId"

            var entityParameter = Expression.Parameter(typeof(TEntity));
            var contextIdProperty = Expression.Property(entityParameter, CONTEXT_ID);

            var contextIdConstant = Expression.Constant(contextId);            
            var comparison = Expression.Equal(contextIdProperty, contextIdConstant);

            return Expression.Lambda<Func<TEntity, bool>>(comparison, new[] { entityParameter });
        }

        private static Func<ObjectContext, int> getContextId;

        public static IQueryable<T> InContext<T>(this ObjectSet<T> objectSet) where T:class
        {
            // Convention: Filter on ContexId if property is present.
            // Don't want to introduce IContextAware interface.

            // Create lambda: (context) => ((DataContextModelContainer)context).ContextId;
            if (getContextId == null)
            {
                var containerContextIdProperty = objectSet.Context.GetType().GetProperty(CONTEXT_ID);

                if (containerContextIdProperty == null)
                    throw new InvalidOperationException("Then entity framework model must have a public ContextId property. Implement this in a partial class.");

                var getter = containerContextIdProperty.GetGetMethod();
                
                var entityExpression = Expression.Parameter(typeof(ObjectContext));
                var castEntity = Expression.ConvertChecked(entityExpression, typeof(DataContextModelContainer));

                var extract = Expression.Call(castEntity, getter);

                var lambda = Expression.Lambda<Func<ObjectContext, int>>(extract, entityExpression).Compile();
                getContextId = lambda;
            }            
           
            var modelContextId = getContextId.Invoke(objectSet.Context);

            return objectSet.Where(In<T>(modelContextId));
        }
    }
}