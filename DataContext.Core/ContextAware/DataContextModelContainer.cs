using System;
using System.Data;
using System.Data.Objects;
using System.Linq.Expressions;

namespace DataContext.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using DataContext.Core.ContextAware;

    public partial class DataContextModelContainer
    {      
        private CurrentContext CurrentContext { get; set; }

        public int ContextId
        {
            get { return CurrentContext.Id; }
        }

        public int Context
        {
            get { return CurrentContext.Id; }
        }

        public DataContextModelContainer(CurrentContext currentContext)
            : this()
        {
            CurrentContext = currentContext;
        }

        partial void OnContextCreated()
        {
            SavingChanges += OnSavingChanges;
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            foreach (ObjectStateEntry entry in ((ObjectContext)sender).ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                if (entry.IsRelationship)
                    continue;

                var entity = entry.Entity;

                if (entity == null)
                    continue;

                // Faster than pure Reflection
                SetContextIdWithCachedCompiledLambda(entity);
            }
        }

        private static Dictionary<Type, Action<object, int>> PropertySetters = new Dictionary<Type, Action<object, int>>();

        private void SetContextIdWithCachedCompiledLambda(object entity)
        {
            if (CurrentContext == null) 
                return;

            var entityType = entity.GetType();

            // Generate and cache setter lambda
            // Not thread safe access to cache - yet
            if (!PropertySetters.ContainsKey(entityType))
            {
                var contextIdProperty = entityType.GetProperty("ContextId");

                if (contextIdProperty == null)
                {
                    PropertySetters.Add(entityType, null);
                    return;
                }

                // Create expression: (entity, contextId) => ((Type)entity).set_ContextId = contextId

                var getsetMethod = contextIdProperty.GetSetMethod();

                var entityExpression = Expression.Parameter(typeof(object));
                var parameterExpression = Expression.Parameter(typeof(int), "contextId");

                var castEntity = Expression.ConvertChecked(entityExpression, entityType);

                var assignment = Expression.Call(castEntity, getsetMethod, new[] { parameterExpression });

                Action<object, int> lambda = Expression.Lambda<Action<object, int>>(assignment, entityExpression, parameterExpression).Compile();

                PropertySetters.Add(entityType, lambda);
            }

            var setter = PropertySetters[entity.GetType()];

            if (setter == null) 
                return;

            setter.Invoke(entity, ContextId);
        }

        public Expression<Func<TEntity, bool>> InContext<TEntity>(TEntity entity)
        {
            var entityParameter = Expression.Parameter(typeof(TEntity));
            var contextId = Expression.Constant(CurrentContext.Id, typeof(int));

            var comparison = Expression.Equal(entityParameter, contextId);

            return Expression.Lambda<Func<TEntity, bool>>(comparison, new[] { entityParameter });
        }

        public IQueryable<T> WithContextFilterOn<T>(Expression<Func<DataContextModelContainer, ObjectSet<T>>> objectSetExpression) where T : class
        {
            var body = objectSetExpression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("Parameter expr must be a memberexpression");

            var objectSetName = body.Member.Name;

            var property = this.GetType().GetProperty(objectSetName);

            var objectSet = property.GetValue(this, null) as ObjectSet<T>;

            return objectSet.In(this.Context);
        }
    }
}
