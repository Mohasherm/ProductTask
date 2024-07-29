using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductTask.SqlServer.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTask.SqlServer.Seed
{

    public class Seeder
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {

            var _context = serviceProvider.GetService<DataContext>();
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync();
            }

            if (!_context.Roles.Any())
                await SeedMethods.SeedRoles(_context);
            if (!_context.Users.Any())
                await SeedMethods.SeedUsers(_context);
            if (!_context.WebContents.Any())
                await SeedMethods.SeedWebContents(_context);
        }
    }

}