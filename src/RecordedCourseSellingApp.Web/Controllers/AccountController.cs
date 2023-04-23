using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(ILogger<AccountController> logger,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> Create()
    {
        var user = new ApplicationUser()
        {
            FirstName = "Mehedi",
            LastName = "Hasan",
            Email = "mehedi@gmail.com",
            UserName = "mehedi@gmail.com"
        };

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            
        }
        
        return View();
    }
}
