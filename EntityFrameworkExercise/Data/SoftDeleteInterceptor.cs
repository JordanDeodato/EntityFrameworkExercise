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
            foreach (var entry in eventData.Context?.ChangeTracker.Entries() ?? Enumerable.Empty<EntityEntry>())
            {
                if (entry is { State: EntityState.Deleted, Entity: IEntitySoftDelete entity })
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
