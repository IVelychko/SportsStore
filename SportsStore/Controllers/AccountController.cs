using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;

    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
    {
        this.userManager = userMgr;
        this.signInManager = signInMgr;
    }

#pragma warning disable CA1054 // URI-like parameters should not be strings
    public ViewResult Login(string returnUrl)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        return this.View(new LoginModel
        {
            Name = string.Empty,
            Password = string.Empty,
            ReturnUrl = returnUrl,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (this.ModelState.IsValid && loginModel is not null)
        {
            IdentityUser? user = await this.userManager.FindByNameAsync(loginModel.Name ?? string.Empty);
            if (user != null)
            {
                await this.signInManager.SignOutAsync();
                if ((await this.signInManager.PasswordSignInAsync(user, loginModel.Password ?? string.Empty, false, false)).Succeeded)
                {
                    return this.Redirect(loginModel.ReturnUrl ?? "/Admin");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid name or password");
        }

        return this.View(loginModel);
    }

    [Authorize]
#pragma warning disable CA1054 // URI-like parameters should not be strings
    public async Task<RedirectResult> Logout(string returnUrl = "/")
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        await this.signInManager.SignOutAsync();
        return this.Redirect(returnUrl);
    }
}
