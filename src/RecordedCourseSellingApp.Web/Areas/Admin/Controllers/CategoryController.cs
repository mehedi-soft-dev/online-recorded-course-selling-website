using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Areas.Admin.Models;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ILifetimeScope _scope;

    public CategoryController(ILogger<CategoryController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Create()
    {
        var model = _scope.Resolve<CategoryCreateModel>();
        model.Name = "Programming";
        model.Description = "This is test description";

        await model.CreateCategory();

        return View();
    }

    public async Task<JsonResult> GetCategoryData()
    {
        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<CategoryListModel>();
        return Json(await model.GetCategoriesPagedData(dataTableModel));
    }
}
