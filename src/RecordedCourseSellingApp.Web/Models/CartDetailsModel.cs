using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Web.Models;

public class CartDetailsModel : BaseModel
{
    public List<CartItemListModel> CartItems { get; set; } = new();

    private IEnrollmentService _enrollmentService;

    public CartDetailsModel() : base()
    {

    }

    public CartDetailsModel(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _enrollmentService = scope.Resolve<IEnrollmentService>();
    }

    public async Task LoadDataAsync(string username)
    {
        var results = await _enrollmentService.GetCartItemsAsync(username);

        foreach (var item in results)
        {
            CartItems.Add(item.Adapt<CartItemListModel>());
        }
    }
}
