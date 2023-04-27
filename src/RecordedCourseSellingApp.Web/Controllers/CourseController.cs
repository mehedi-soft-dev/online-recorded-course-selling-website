using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecordedCourseSellingApp.Web.Controllers;

[Authorize]
public class CourseController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
}
