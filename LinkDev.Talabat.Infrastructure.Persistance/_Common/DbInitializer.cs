using LinkDev.Talabat.Core.Domain.Contracts.Presistence.DbInitializers;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
    public abstract class DbInitializer(DbContext _DbContext) : IDbInitializer
    {
        public async Task InitializAsync()
        {
            var PendingMigrations = _DbContext.Database.GetPendingMigrations();

            if (PendingMigrations.Any())
                await _DbContext.Database.MigrateAsync();
        }
        

        public abstract Task SeedAsync();
        
    }
}
