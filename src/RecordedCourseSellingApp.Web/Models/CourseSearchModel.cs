using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.Common;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Web.Models;

public class CourseSearchModel : BaseModel
{
    public CourseSearchItem SearchItem { get; set; } = new();

    public List<AvailableCourse>? AvailableCourses { get; set; } = new();

    public ICourseService _courseService;

    public ICategoryService _categoryService;

    public CourseSearchModel() : base() 
    {
    
    }

    public CourseSearchModel(ICourseService courseService, ICategoryService categoryService)
    {
        _courseService = courseService;
        _categoryService = categoryService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _categoryService = _scope.Resolve<ICategoryService>();
        _courseService = _scope.Resolve<ICourseService>();
    }

    internal async Task SearchDataAsync()
    {
        var courses = await _courseService.GetCoursesBySearchAsync(SearchItem.CategoryId, 
            SearchItem.DifficultyLevel, SearchItem.SearchText);

        foreach(var course in courses)
        {
            AvailableCourses!.Add(course.Adapt<AvailableCourse>());
        }
    }

    internal async Task LoadDdlAsync()
    {
        SearchItem.CategoryDdl = await _categoryService!.GetCategoriesAsDdlAsync();
        SearchItem.DificultyLevelDdl = DdlService.DifficultyLevelDdl;
    }
}

public class AvailableCourse
{
	public Guid CategoryId { get; set; }
	public string CategoryName { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public DifficultyLevel DifficultyLevel { get; set; }
	public int Price { get; set; }
	public string? ThumbnailImage { get; set; }
}
