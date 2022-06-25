using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Identity;

public class RegisterUserViewModel
{
    [Required]
    [Display(Name = "Name")]
    public string UserName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Repeate Password")]
    [Compare(nameof(Password), ErrorMessage ="Passwords doesn't match")]
    public string PasswordConfirm { get; set; } = null!;
}
