using Autofac;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordedCourseSellingApp.Web.Areas.Identity.Models;

public class SignInModel : BaseModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
    }
}
