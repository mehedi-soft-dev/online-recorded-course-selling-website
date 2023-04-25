using Microsoft.AspNetCore.Identity;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.DataAccess.Seeds;

public static class ApplicationAdminUserSeed
{
    public static ApplicationUser[] GetUsers
    {
        get
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser()
            {
                FirstName = "Admin",
                Email = "admin@eedu.com",
                UserName = "admin@eedu.com",
                NormalizedEmail = "ADMIN@EEDU.COM",
                NormalizedUserName = "ADMIN@EEDU.COM",
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
            };

            var passwordHash = passwordHasher.HashPassword(user, "Admin@123");
            user.PasswordHash = passwordHash;

            return new ApplicationUser[] { user };
        }
    }
}
