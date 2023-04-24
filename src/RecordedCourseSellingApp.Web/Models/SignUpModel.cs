using Autofac;
using System.ComponentModel.DataAnnotations;

namespace RecordedCourseSellingApp.Web.Models;

public sealed class SignUpModel : BaseModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Image { get; set; }
    public string? ReturnUrl { get; set; }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
    }
}
