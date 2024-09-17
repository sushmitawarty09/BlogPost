using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Data
{
    public class BlogPageAuthDBContext : IdentityDbContext
    {
        public BlogPageAuthDBContext(DbContextOptions<BlogPageAuthDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var superAdminRoleId = "ae340858-b307-48c4-a846-403857937c4a";
            var adminRoleId = "6959bf5a-77b3-4043-9307-41271af92c11";
            var userRoleId = "e8305fd1-b6ef-4b8d-adc1-3861b11b1fba";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            var superUserId = "92fb9251-9bc0-4651-98b8-3e441c0dda62";
            var superAdminUser = new IdentityUser
            {
                Id=superUserId,
                UserName="SuperAdmin@test.com",
                Email="SuperAdmin@test.com",
                NormalizedUserName= "SuperAdmin@test.com",
                NormalizedEmail= "SuperAdmin@test.com"
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId=superUserId
                },
                {
                    new IdentityUserRole<string>
                    {
                        RoleId=adminRoleId,
                        UserId=superUserId
                    }
                },
                {
                    new IdentityUserRole<string>
                    {
                        RoleId=userRoleId,
                        UserId=superUserId
                    }
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
