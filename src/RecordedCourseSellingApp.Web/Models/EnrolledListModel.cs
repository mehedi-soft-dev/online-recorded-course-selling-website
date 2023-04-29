using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Web.Models;

public class EnrolledListModel : BaseModel
{
    public List<EnrolledCourse> EnrolledCourses { get; set; } = new();

    private IEnrollmentService _enrollmentService;

    public EnrolledListModel() : base()
    {
        
    }

    public EnrolledListModel(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _enrollmentService = scope.Resolve<IEnrollmentService>();
    }

    internal async Task LoadDataAsync(string username)
    {
        var results = await _enrollmentService.GetEnrolledCoursesAsync(username);

        foreach (var item in results)
        {
            EnrolledCourses.Add(item.Adapt<EnrolledCourse>());
        }
    }
}

public class EnrolledCourse
{
    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public string ThumbnailImage { get; set; }

    public string CategoryName { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }
}
