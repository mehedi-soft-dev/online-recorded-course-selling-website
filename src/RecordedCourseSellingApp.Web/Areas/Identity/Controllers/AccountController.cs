using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.Web.Areas.Identity.Models;
using System.Text;

namespace RecordedCourseSellingApp.Web.Areas.Identity.Controllers;

[Area("Identity")]
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
                
                await _userManager.AddToRoleAsync(user, "User");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null,
                    values: new { area = "", userId = user.Id, code, returnUrl = model.ReturnUrl },
                    protocol: Request.Scheme);

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl }); ;
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if(model.ReturnUrl == null)
                        return RedirectToAction("Index", "Course", new {Area = ""});

                    return RedirectToAction(model.ReturnUrl);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(string? returnUrl = null)
    {
        var model = _scope.Resolve<SignInModel>();
        model.ReturnUrl = returnUrl;

        await _signInManager.SignOutAsync();

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(SignInModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    _logger.LogInformation("Admin Logged In");

                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }

                _logger.LogInformation("User logged in.");


                if (model.ReturnUrl != null)
                    return LocalRedirect(model.ReturnUrl);

                return RedirectToAction("","", new {Area = ""});
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("LoginWith2fa", new { model.ReturnUrl, model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToAction("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignOut(string? returnUrl = null)
    {
        await _signInManager.SignOutAsync();

        if (returnUrl != null)
            return LocalRedirect(returnUrl);

        return RedirectToAction("Index", "Course", new { Area = "" });
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
