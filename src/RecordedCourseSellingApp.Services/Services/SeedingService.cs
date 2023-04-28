using Microsoft.AspNetCore.Identity;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.DataAccess.Seeds;

namespace RecordedCourseSellingApp.Services.Services;

public class SeedingService : ISeedingService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedingService(RoleManager<ApplicationRole> roleManager, 
        UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        await SeedApplicationRole();
        await SeedAdminUser();
    }

    private async Task SeedApplicationRole()
    {
        var roles = ApplicationRoleSeed.GetRoles;

        foreach (var role in roles)
        {
            var isExist = await _roleManager.RoleExistsAsync(role.NormalizedName);

            if (!isExist) 
                await _roleManager.CreateAsync(role);
        }
    }

    private async Task SeedAdminUser() 
    {
        var users = ApplicationAdminUserSeed.GetUsers;

        foreach (var user in users)
        {
            var result = await _userManager.FindByNameAsync(user.NormalizedUserName);

            if(result == null)
            {
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}
