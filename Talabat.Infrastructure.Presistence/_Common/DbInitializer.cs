using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace Talabat.Infrastructure.Presistence._Common
{
    internal class DbInitializer(DbContext DbContext) :IDbInitializer
    {
        public virtual async Task InitalizeAsync()
        {
            var pendingMigrations = await DbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await DbContext.Database.MigrateAsync();
        }

        public virtual Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
