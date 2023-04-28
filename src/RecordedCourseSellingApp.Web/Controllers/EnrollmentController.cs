using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

    public async Task<IActionResult> AddToCart(Guid CourseId)
    {
        try
        {
            var model = _scope.Resolve<CartItemAddModel>();
            model.CourseId = CourseId;
            var cartItems = await model.AddToCartAsync(User.Identity!.Name!);

            HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(cartItems));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return RedirectToAction("Details", "Course", new { Area = "", id = CourseId });
    }
}
