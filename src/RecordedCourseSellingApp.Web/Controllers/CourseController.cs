using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Controllers;

//[Authorize(Roles ="User")]
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

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return View(model);
    }
}
