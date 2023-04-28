using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Controllers;

[Authorize(Roles = "User")]
public class EnrollmentController : Controller
{
    private readonly ILogger _logger;
    private readonly ILifetimeScope _scope;

    public EnrollmentController(ILogger<EnrollmentController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    public async Task<JsonResult> AddToCart(Guid CourseId)
    {
        try
        {
            var model = _scope.Resolve<CartItemAddModel>();
            model.CourseId = CourseId;
            await model.AddToCartAsync(User.Identity!.Name!);

            return Json(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return Json(false);
    }

    public async Task<JsonResult> GetUserCartItems(Guid CourseId)
    {
        return Json(false);
    }
}
