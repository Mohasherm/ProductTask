using Microsoft.EntityFrameworkCore;
using ProductTask.Entity.Main;
using ProductTask.Entity.Permission;
using ProductTask.Entity.Security;

namespace ProductTask.SqlServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasQueryFilter(x => x.IsValid);
            modelBuilder.Entity<RoleModel>().HasQueryFilter(x => x.IsValid);
            modelBuilder.Entity<ProductModel>().HasQueryFilter(x => x.IsValid);
            modelBuilder.Entity<CategoryModel>().HasQueryFilter(x => x.IsValid);
            modelBuilder.Entity<ProductCategoryModel>().HasQueryFilter(x => x.IsValid);
            modelBuilder.Entity<WebContentModel>().HasQueryFilter(x => x.IsValid);
            modelBuilder.Entity<WebContentRoleModel>().HasQueryFilter(x => x.IsValid);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductCategoryModel> ProductCategories { get; set; }
        public DbSet<WebContentModel> WebContents { get; set; }
        public DbSet<WebContentRoleModel> WebContentRoles { get; set; }

    }
}
