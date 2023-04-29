using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Shared.Exceptions;
using RecordedCourseSellingApp.Web.Areas.Admin.Models;
using RecordedCourseSellingApp.Web.Extensions;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CourseController : Controller
{
    private readonly ILogger<CourseController> _logger;
    private readonly ILifetimeScope _scope;

    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
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
        var model = _scope.Resolve<CourseCreateModel>();
        await model.LoadDdlAsync();

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseCreateModel model, IFormFile file)
    {
        model.ResolveDependency(_scope);

        if (file is not null)
            model.ThumbnailImage = await model.UploadFileAsync(file);

        if (ModelState.IsValid)
        {
            try
            {
                await model.CreateCourseAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "New course successfully created",
                    Type = ResponseTypes.Success
                });
            }
            catch (DuplicationExeption ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There is a problem in creating course",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction(nameof(Index));
        }

        await model.LoadDdlAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseEditModel>();
            await model.LoadDataAsync(id);
            await model.LoadDdlAsync();

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "There is a problem in editing course",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CourseEditModel model, IFormFile? file)
    {
        model.ResolveDependency(_scope);

        if (file is not null)
            model.ThumbnailImage = await model.UploadFileAsync(file);

        if (ModelState.IsValid)
        {
            try
            {
                await model.EditCourseAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Course successfully edited",
                    Type = ResponseTypes.Success
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There is a problem in editing course",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction(nameof(Index));
        }

        await model.LoadDdlAsync();
        return View(model);
    }

    [ValidateAntiForgeryToken, HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseListModel>();
            await model.DeleteCourseAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Course successfully deleted",
                Type = ResponseTypes.Success
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "There is a problem in deleting course",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<JsonResult> GetCourseData()
    {
        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<CourseListModel>();
        return Json(await model.GetCategoriesPagedData(dataTableModel));
    }
}