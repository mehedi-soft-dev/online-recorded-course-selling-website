using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Web.Models;

public class CourseDetailsModel : BaseModel
{
    public Guid CourseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int Price { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }

    public string? ThumbnailImage { get; set; }

    public string VideoLink { get; set; } = string.Empty;

    public bool AlreadyAddedToCart { get; set; }

    public bool AlreadyEnrolled { get; set; }

    private ICourseService _courseService;

    
    public CourseDetailsModel() : base()
    {

    }

    public CourseDetailsModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
    }

    public async Task LoadDataAsync(string? username = null)
    {
        if (CourseId == Guid.Empty)
            throw new Exception("Course Id can't be null");

        var course = await _courseService.GetCourseDetailsByIdAsync(CourseId, username);

        course.Adapt(this);
    }
}
