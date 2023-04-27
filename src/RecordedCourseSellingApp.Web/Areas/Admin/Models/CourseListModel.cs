using Autofac;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Models;

public class CourseListModel : BaseModel
{
    public ICourseService _courseService;

    public CourseListModel() : base()
    {

    }

    public CourseListModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
    }

    internal async Task DeleteCourseAsync(Guid id)
    {
        await _courseService.DeleteCourseAsync(id);
    }

    internal async Task<object?> GetCategoriesPagedData(DataTablesAjaxRequestModel dataTablesModel)
    {
        var data = await _courseService.GetCoursesByPagingAsync(
            dataTablesModel.PageIndex,
            dataTablesModel.PageSize,
            dataTablesModel.SearchText,
            dataTablesModel.GetSortText(new string[] { "ThumbnailImage", "Title", "Category", "DifficultyLevel", "Price" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
                    select new string[]
                    {
                        record.ThumbnailImage != null ? "/UploadedFiles/Course/Thumbnail/" + record.ThumbnailImage : "",
                        record.Title,
                        record.Categoryname,
                        record.DifficultyLevel.ToString(),
                        record.Price.ToString("0,000"),
                        record.Id.ToString(),
                    }).ToArray()
        };
    }
}
