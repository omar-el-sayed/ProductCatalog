using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL.Context;
using ProductCatalog.DAL.Models;

namespace ProductCatalog.DAL
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;
        private readonly ProductCatalogContext _context;

        public DbInitializer(ModelBuilder builder, ProductCatalogContext context)
        {
            _builder = builder;
            _context = context;
        }

        public void Seed()
        {
            _builder.Entity<Category>(a =>
            {
                a.HasData(new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    CreatedOn = DateTime.Now
                });
                a.HasData(new Category
                {
                    Id = 2,
                    Name = "Sports",
                    CreatedOn = DateTime.Now
                });
                a.HasData(new Category
                {
                    Id = 3,
                    Name = "Video Games",
                    CreatedOn = DateTime.Now
                });
            });

            _builder.Entity<IdentityRole>(a =>
            {
                a.HasData(new IdentityRole
                {
                    Id = "1903d8cc-f9b4-4a82-b653-d35e2a75e3d6",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
                a.HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
            });

            _builder.Entity<ApplicationUser>(a =>
            {
                a.HasData(new ApplicationUser
                {
                    Id = "673f1e40-23fc-4ca1-9317-553be38732a7",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "admin@admin.com".ToUpper(),
                    Email = "admin@admin.com",
                    NormalizedEmail = "admin@admin.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDjWvBH3YhUqptDaomlc0uj3Cjl7n+AX38kPPVxuMqJstQZsdJHf7WHeupcxlGLXIA==", // admin@123
                    SecurityStamp = "VCTV7GBERPI2DC22ZVSHWLRFJDIRWMK2",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    FullName = "Admin User"
                });
            });

            _builder.Entity<IdentityUserRole<string>>(a =>
            {
                a.HasData(new IdentityUserRole<string>
                {
                    UserId = "673f1e40-23fc-4ca1-9317-553be38732a7",
                    RoleId = "1903d8cc-f9b4-4a82-b653-d35e2a75e3d6"
                });
            });
        }
    }
}
