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

    internal async Task AddToCartAsync(string username)
    {
        Username = username;
        var cartItemBO = this.Adapt<CartItem>();

        await _enrollmentService.AddCartItemAsync(cartItemBO);
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
