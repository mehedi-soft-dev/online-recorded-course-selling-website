using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace RecordedCourseSellingApp.Web.Models;

public class CheckoutCreateModel : BaseModel
{
    public string Username { get; set; }
    
    public int CartItemCount { get; set; }

    [Display(Name = "Total Amount")]
    public int CartItemPriceTotal { get; set; }

    [Required, Display(Name ="Card Number")]
    public string CardNumber { get; set; }

    [Required, Display(Name = "Card Expire Date")]
    public string CardExpireDate { get; set; }
    
    [Required, Display(Name = "Card Pin")]
    public string CardPin { get; set; }
    
    public int TotalAmount { get; set; }

    public virtual DateTime EntryDate { get; set; }

    private IEnrollmentService _enrollmentService;

    public CheckoutCreateModel() : base()
    {
        
    }

    public CheckoutCreateModel(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _enrollmentService = scope.Resolve<IEnrollmentService>();
    }

    internal async Task CreateCheckoutAsync(string username)
    {
        await _enrollmentService.CreateCheckoutAsync(username);
    }

    public async Task GetCartItemCoutAsync(string username)
    {

    }

    internal async Task LoadDataAsync(string username)
    {
        var data = await _enrollmentService.GetCheckoutDataAsync(username);

        CartItemCount = data.CartItems;
        CartItemPriceTotal = data.TotalAmount;
    }
}
