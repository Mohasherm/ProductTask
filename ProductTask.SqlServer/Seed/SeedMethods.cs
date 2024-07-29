using ButterflyApi.SharedKernal.Helper;
using Microsoft.EntityFrameworkCore;
using ProductTask.Entity.Permission;
using ProductTask.Entity.Security;
using ProductTask.Shared.Enums;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.SqlServer.Seed
{
    public class SeedMethods
	{
		public static async Task SeedRoles(DataContext context)
		{
			var rolesNames = Enum.GetNames(typeof(Role));
			var roles = new List<RoleModel>();
			foreach (var roleName in rolesNames)
			{
				var role = new RoleModel
				{
					Id = Guid.NewGuid(),
					Name = roleName.ToString()
				};
				roles.Add(role);
			}
			context.Roles.AddRange(roles);
			await context.SaveChangesAsync();
		}

		public static async Task SeedUsers(DataContext context)
		{
			var AdminRole = await context.Roles.FirstOrDefaultAsync(s => s.Name == Role.Admin.ToString());
			var user = new UserModel
			{
				Id = Guid.NewGuid(),
				Password = PasswordHelper.HashPassword("Aaa@123"),
				UserName = "Admin",
				RoleId = AdminRole.Id,
			};
			context.Add(user);
			await context.SaveChangesAsync();
		}

        public static async Task SeedWebContents(DataContext context)
        {
            var controllers = Enum.GetNames(typeof(ControllerNames));

            var webContentes = new List<WebContentModel>();
            var webContentesRoles = new List<WebContentRoleModel>();

            foreach (var item in controllers)
            {
                webContentes.Add(new WebContentModel
                {
                    Id = Guid.NewGuid(),
                    Name = item
                });
            }
            context.AddRange(webContentes);
            var roles = await context.Roles.ToListAsync();
            foreach (var role in roles)
            {
                foreach (var item in webContentes)
                {
                    if (role.Name == Role.Admin.ToString())
                        webContentesRoles.Add(new WebContentRoleModel
                        {
                            IsValid = true,
                            RoleId = role.Id,
                            WebContentId = item.Id,
                            CanAdd = true,
                            CanDelete = true,
                            CanEdit = true,
                            CanView = true,
                        });
                    else
                        webContentesRoles.Add(new WebContentRoleModel
                        {
                            IsValid = true,
                            RoleId = role.Id,
                            WebContentId = item.Id,
                            CanAdd = false,
                            CanDelete = false,
                            CanEdit = false,
                            CanView = true,
                        });
                }
            }
            context.AddRange(webContentesRoles);
            await context.SaveChangesAsync();
        }
    }
}
