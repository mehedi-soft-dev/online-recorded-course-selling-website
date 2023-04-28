using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Controllers;

[AllowAnonymous]
public class CourseController : Controller
{
    private readonly ILogger<CourseController> _logger;
    private readonly ILifetimeScope _scope;

    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
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
            await model.LoadDataAsync();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return RedirectToAction("Index", "Course", new { Area = ""});
    }
}
