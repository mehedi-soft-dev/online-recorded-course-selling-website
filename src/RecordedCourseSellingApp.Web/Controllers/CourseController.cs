using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Controllers;

[AllowAnonymous]
public class CourseController : Controller
{
    private readonly ILogger<CourseController> _logger;
    private readonly ILifetimeScope _scope;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _scope = scope;
        _userManager = userManager;
    }

    [HttpGet]
	public async Task<IActionResult> Index()
    {
        var model = _scope.Resolve<CourseSearchModel>();
        await model.LoadDdlAsync();
        await model.SearchDataAsync();

        return View(model);
    }

	[HttpPost]
    [AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CourseSearchModel? model = null)
    {
        try
        {
            model!.ResolveDependency(_scope);
            await model.LoadDdlAsync();
            await model.SearchDataAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseDetailsModel>();
            model.CourseId = id;

            string? username = null;

            if (!string.IsNullOrEmpty(User.Identity?.Name))
                username = User.Identity?.Name;

            await model.LoadDataAsync(username);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return RedirectToAction("Index", "Course", new { Area = ""});
    }

    [HttpGet]
    public async Task<IActionResult> Cart()
    {
        return View();
    }
}
