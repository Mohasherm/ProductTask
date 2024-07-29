using ButterflyApi.SharedKernal.Helper;
using Microsoft.EntityFrameworkCore;
using ProductTask.Entity.Security;
using ProductTask.Shared.Enums;
using ProductTask.SqlServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTask.SqlServer.Seed
{
    public class SeedMethods
	{
		public static void SeedRoles(DataContext context)
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
			context.SaveChanges();
		}

		public static void SeedUsers(DataContext context)
		{
			var AdminRole =  context.Roles.FirstOrDefault(s => s.Name == Role.Admin.ToString());
			var user = new UserModel
			{
				Id = Guid.NewGuid(),
				Password = PasswordHelper.HashPassword("Aaa@123"),
				UserName = "Admin",
				RoleId = AdminRole.Id,
			};
			context.Add(user);
			context.SaveChanges();
		}

        
    }
}
