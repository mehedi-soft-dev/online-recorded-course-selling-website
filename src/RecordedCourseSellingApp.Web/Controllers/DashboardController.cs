using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Controllers;

[Authorize(Roles = "User")]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly ILifetimeScope _scope;

    public DashboardController(ILogger<DashboardController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    public async Task<IActionResult> Index()
    {
        var model = _scope.Resolve<EnrolledListModel>();
        await model.LoadDataAsync(User.Identity!.Name!);

        return View(model);
    }

    public async Task<IActionResult> Watch(Guid id)
    {
        try
        {
            var model = _scope.Resolve<EnrolledCourseDetailModel>();
            await model.LoadDataAsync(User.Identity!.Name!, id);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return RedirectToAction("Index");
    }
}
