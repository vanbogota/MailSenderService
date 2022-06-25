using FinalProject.Models.Identity;
using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager, 
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    #region Регистрация
    public IActionResult Register() => View(new RegisterUserViewModel());

    [HttpPost]
    public async Task<ActionResult> Register(RegisterUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = new User
        {
            UserName = viewModel.UserName,
            Email = viewModel.Email
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("Пользователь {0} успешно создан.", user);
            
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        return View(viewModel);
    }

    #endregion

    #region Вход в систему
    public IActionResult Login(string? returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl});

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var result = await _signInManager.PasswordSignInAsync(
            viewModel.UserName,
            viewModel.Password,
            viewModel.RememberMe,
            true);

        if (result.Succeeded)
        {
            _logger.LogInformation("Пользователь {0} успешно вошел в систему", viewModel.UserName);
            return LocalRedirect(viewModel.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Login or password failed");
        _logger.LogWarning("Ошибка при входе в систему {0}", viewModel.UserName);
        return View(viewModel);
    }

    #endregion

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    public IActionResult AccessDenied() => View();


}
