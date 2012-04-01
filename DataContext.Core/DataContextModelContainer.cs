using System;
using System.Data;
using System.Data.Objects;
using System.Linq.Expressions;

namespace DataContext.Core
{
    public partial class DataContextModelContainer
    {
        private CurrentContext CurrentContext { get; set; }

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
            foreach (ObjectStateEntry entry in ((ObjectContext)sender).ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (entry.IsRelationship)
                    continue;

                var entity = entry.Entity;

                if (entity == null)
                    continue;

                SetContextId(entity);
            }
        }

        private void SetContextId(object entity)
        {
            if (CurrentContext == null) 
                return;

            var contextIdProperty = entity.GetType().GetProperty("ContextId");

            if (contextIdProperty == null)
                return;

            contextIdProperty.SetValue(entity, CurrentContext.Id, null);
        }

        public Expression<Func<TEntity, bool>> InContext<TEntity>(TEntity entity)
        {
            var entityParameter = Expression.Parameter(typeof(TEntity));
            var contextId = Expression.Constant(CurrentContext.Id, typeof(int));

            var comparison = Expression.Equal(entityParameter, contextId);

            return Expression.Lambda<Func<TEntity, bool>>(comparison, new[] { entityParameter });
        }
    }
}
