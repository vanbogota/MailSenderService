using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Identity;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Name")]
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string? ReturnUrl { get; set; }
}
