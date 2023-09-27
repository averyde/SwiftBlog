using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed roles (User, Admin, SuperAdmin)
            var adminRoleId = "030f4642-6891-46a9-a8fe-41e8205103e1";
            var superAdminRoleId = "ec2156d2-53c2-4266-a36c-6f9008abfd34";
            var userRoleId = "36714c76-6a8b-42df-a985-a24f2ca2b473";

            var roles = new List<IdentityRole> 
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // seed SuperAdminUser

            var superAdminId = "643af168-c76b-48f8-9b5b-ba87d8722413";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@swiftblog.com",
                Email = "superadmin@swiftblog.com",
                NormalizedUserName = "superadmin@swiftblog.com".ToUpper(),
                NormalizedEmail = "superadmin@swiftblog.com".ToUpper(),
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Dekuleaf01");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
