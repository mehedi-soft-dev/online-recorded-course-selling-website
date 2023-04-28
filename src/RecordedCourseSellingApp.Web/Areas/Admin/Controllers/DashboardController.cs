using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Areas.Admin.Models;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
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
        var model = _scope.Resolve<DashboardModel>();

        await model.LoadDataAsync();

        return View(model);
    }
}
