using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Domain.Common;

namespace Talabat.Infrastructure.Presistence.Data.Interceptors
{
    internal class BaseAuditableEntityInterceptor(ILogedInUserService logedInUserService) :SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntites(eventData.Context);
            return base.SavedChanges(eventData, result);
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntites(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        private void UpdateEntites(DbContext? dbContext)
        {
            if (dbContext is null) return;
            var UtcNow = DateTime.UtcNow;
            foreach (var entity in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {
                if (entity is { State: EntityState.Added or EntityState.Modified })
                {
                    if (entity.State is EntityState.Added)
                    {
                        entity.Entity.CreatedBy = "";
                        entity.Entity.CreatedOn = UtcNow;
                    }
                    entity.Entity.LastModifiedBy = "";
                    entity.Entity.LastModifiedOn = UtcNow;
                }
            }

        }
    }
}
