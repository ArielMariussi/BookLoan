using EmprestimoLivros.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmprestimoLivros.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Invalid email or password");
        return View(model);

    }

    [HttpGet]
    public IActionResult Register()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            TempData["SuccessMessage"] = "Account created successfully!";
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginDemo()
    {
        var demoUser = await _userManager.FindByEmailAsync("demo@demo.com");

        if (demoUser == null)
        {
            ModelState.AddModelError(string.Empty, "Demo account is not available right now");
            return View("Login");
        }

        await _signInManager.SignInAsync(demoUser, isPersistent: false);
        TempData["SuccessMessage"] = "Welcome to Demo Mode!";
        return RedirectToAction("Index", "Home");
    }
}
