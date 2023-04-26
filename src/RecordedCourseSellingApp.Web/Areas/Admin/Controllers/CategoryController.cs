using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Areas.Admin.Models;
using RecordedCourseSellingApp.Web.Extensions;
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

    public IActionResult Create()
    {
        var model = _scope.Resolve<CategoryCreateModel>();

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            await model.CreateCategoryAsync();

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "New category successfully created",
                Type = ResponseTypes.Success
            });

            return RedirectToAction(nameof(Index));

        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var model = _scope.Resolve<CategoryEditModel>();
        await model.LoadDataAsync(id);

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            await model.EditCategoryAsync();

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Category successfully edited",
                Type = ResponseTypes.Success
            });

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    public async Task<JsonResult> GetCategoryData()
    {
        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<CategoryListModel>();
        return Json(await model.GetCategoriesPagedData(dataTableModel));
    }
}
