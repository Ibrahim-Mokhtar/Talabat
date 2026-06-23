using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace Talabat.Infrastructure.Presistence._Common
{
    internal abstract class DbInitializer(DbContext DbContext) :IDbInitializer
    {
        public async Task InitalizeAsync()
        {
            var pendingMigrations = await DbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await DbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
