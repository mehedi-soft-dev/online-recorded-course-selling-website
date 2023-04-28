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

    public async Task RemoveCartItemAsync(Guid cartItemId)
    {
        await _enrollmentService.RemoveCartItemAsync(cartItemId);
    }

    internal async Task<IList<CartItemListModel>> GetCartItemsAsync(string username)
    {
        var result = await _enrollmentService.GetCartItemsAsync(username);

        IList<CartItemListModel> cartItems = new List<CartItemListModel>();

        foreach (var item in result)
        {
            cartItems.Add(item.Adapt<CartItemListModel>());
        }

        return cartItems;
    }
}
