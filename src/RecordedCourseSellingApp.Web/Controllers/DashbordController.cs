using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecordedCourseSellingApp.Web.Controllers;

[Authorize(Roles = "User")]
public class DashbordController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Watch(Guid id)
    {
        return View();
    }
}
