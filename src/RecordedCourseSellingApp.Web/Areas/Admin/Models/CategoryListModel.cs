using Autofac;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Web.Models;
using System.Security.Claims;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Models;

public class CategoryListModel : BaseModel
{
    public ICategoryService _categoryService;

    public CategoryListModel() : base()
    {

    }

    public CategoryListModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _categoryService = _scope.Resolve<ICategoryService>();
    }

    internal async Task<object?> GetCategoriesPagedData(DataTablesAjaxRequestModel dataTablesModel)
    {
        var data = await _categoryService.GetCategoriesByPagingAsync(
            dataTablesModel.PageIndex,
            dataTablesModel.PageSize,
            dataTablesModel.SearchText,
            dataTablesModel.GetSortText(new string[] { "Name", "Description", "IsActive"}));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
                    select new string[]
                    {
                        record.Name,
                        record.Description == null ? "" : record.Description,
                        record.IsActive ? "Yes" : "No",
                        record.Id.ToString(),
                    }).ToArray()
        };
    }
}
