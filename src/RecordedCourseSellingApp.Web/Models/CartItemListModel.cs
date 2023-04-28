using Autofac;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Web.Models;

public class CartItemListModel : BaseModel
{
    public Guid CartItemId { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Price { get; set; }

    public string? ThumbnailImage { get; set; }

    private IEnrollmentService _enrollmentService;

    public CartItemListModel() : base()
    {
        
    }

    public CartItemListModel(IEnrollmentService enrollmentService)
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

    }
}
