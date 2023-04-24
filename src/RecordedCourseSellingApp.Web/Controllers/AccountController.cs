using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.Web.Models;
using System.Text;

namespace RecordedCourseSellingApp.Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly ILifetimeScope _scope;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(ILogger<AccountController> logger, ILifetimeScope scope,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _scope = scope;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult SignUp(string? returnUrl = null)
    {
        var model = _scope.Resolve<SignUpModel>();
        model.ReturnUrl = returnUrl;
        
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpModel model)
    {
        model.ReturnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null,
                    values: new { area = "", userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                    protocol: Request.Scheme);

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl }); ;
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
}
