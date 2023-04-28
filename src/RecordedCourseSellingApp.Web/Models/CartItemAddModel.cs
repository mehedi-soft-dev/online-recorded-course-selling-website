using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Web.Models;

public class CartItemAddModel : BaseModel
{
    public Guid CartItemId { get; set; }

    public string Username { get; set; }

    public Guid CourseId { get; set; }

    private IEnrollmentService _enrollmentService { get; set; }

    public CartItemAddModel() : base()
    {
        
    }

    public CartItemAddModel(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _enrollmentService = _scope.Resolve<IEnrollmentService>();
    }

    internal async Task<bool> AddToCartAsync(string username)
    {
        Username = username;
        var cartItemBO = this.Adapt<CartItem>();

        await _enrollmentService.AddCartItemAsync(cartItemBO);
        
        return true;
    }
}
