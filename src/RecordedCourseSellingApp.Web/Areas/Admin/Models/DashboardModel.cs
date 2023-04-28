using Autofac;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Models;

public class DashboardModel : BaseModel
{
    public int CategoryTotal { get; set; }

    public int CourseTotal { get; set; }

    private ICategoryService _categoryService;

    private ICourseService _courseService;

    public DashboardModel() : base()
    {

    }

    public DashboardModel(ICategoryService categoryService, ICourseService courseService)
    {
        _categoryService = categoryService;
        _courseService = courseService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _categoryService = scope.Resolve<ICategoryService>();
        _courseService = scope.Resolve<ICourseService>();
    }

    public async Task LoadDataAsync()
    {
        CategoryTotal = await _categoryService.GetCategoryCountAsync();
        CourseTotal = await _courseService.GetCoursesCountAsync();
    }
}
