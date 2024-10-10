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
    internal class CustomSaveChangesInterceptors(ILoggedInUserService loggedInUserService) : SaveChangesInterceptor
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
            if (dbContext is null)
                return;
            foreach(var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
                .Where(entity=>entity.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = loggedInUserService.UserId!;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                entry.Entity.LastModifiedBy = loggedInUserService.UserId!;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
