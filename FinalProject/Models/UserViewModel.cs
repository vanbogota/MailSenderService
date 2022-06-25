using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; } = null!;
        
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(100, MinimumLength = 2, ErrorMessage ="Max length 100 symbols, min length is 2 symbols")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is mandatory")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? PasswordHash { get; set; }        
    }
}
