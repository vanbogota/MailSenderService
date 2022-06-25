using FinalProject.Models;
using FinalProject.Models.Identity;
using Identity.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    
    private readonly ILogger<UserController> _logger;
    
    public UserController(UserManager<User> userManager, ILogger<UserController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
       
    public IActionResult Index()
    {
        List<User> users = _userManager.Users.ToList();
        return View(users);
    }

    public IActionResult Create() => View(new RegisterUserViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(RegisterUserViewModel viewModel)
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

            return RedirectToAction("Details", new { viewModel.UserName });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        return View(viewModel);
    }

    public async Task<IActionResult> Edit(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user == null)
        {
            return NotFound();
        }

        return View(new UserViewModel
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserViewModel model)
    {
        var user = new User
        {
            Id = model.Id,
            UserName = model.Name,
            Email = model.Email,
            PasswordHash = model.PasswordHash
        };

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return NotFound();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(string name) 
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user == null)
        {
            return NotFound();
        }

        return View(new UserViewModel
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        });
    }
    
    public async Task<IActionResult> Delete(string name) 
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user == null)
        {
            return NotFound();
        }
        
        return View(new UserViewModel
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return NotFound();

        return RedirectToAction("Index");
    }
}
