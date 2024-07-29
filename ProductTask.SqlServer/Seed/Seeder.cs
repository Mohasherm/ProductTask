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

            if (_context.Database.GetPendingMigrations().Any())
                _context.Database.Migrate();
           
            if (!_context.Roles.Any())
                SeedMethods.SeedRoles(_context);
            if (!_context.Users.Any())
                 SeedMethods.SeedUsers(_context);
          
        }
    }

}