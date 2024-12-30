using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EntityFrameworkExercise.Models.Interfaces;

namespace EntityFrameworkExercise.Data
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if(context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted && entry.Entity is IEntitySoftDelete entity)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.UtcNow;

                    // State
                    entry.State = EntityState.Modified;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
