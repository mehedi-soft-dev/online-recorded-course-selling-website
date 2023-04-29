using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Web.Models;

public class EnrolledCourseDetailModel : BaseModel
{
    public string Title { get; set; }

    public string VideoUrl { get; set; }

    private IEnrollmentService _enrollmentService;

    public EnrolledCourseDetailModel() : base()
    {
        
    }

    public EnrolledCourseDetailModel(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _enrollmentService = scope.Resolve<IEnrollmentService>();
    }

    internal async Task LoadDataAsync(string username, Guid courseId)
    {
        var enrolledCourse = await _enrollmentService.GetEnrolledCourseDetailsAsync(username, courseId);

        enrolledCourse.Adapt(this);
    }
}
