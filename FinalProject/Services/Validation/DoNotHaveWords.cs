using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Services.Validation
{
    /// <summary>
    /// Prohibition to some words.
    /// </summary>
    public class DoNotHaveWords : ValidationAttribute
    {
        private string[] _words;
        public DoNotHaveWords(string[] words)
        {
            _words = words;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var bad = new HashSet<string>(_words, StringComparer.InvariantCultureIgnoreCase);
            
            var request = (Message)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(request.Body))
            {
                return ValidationResult.Success;
            }

            var words = new HashSet<string>(request.Body.Split(" "), StringComparer.InvariantCultureIgnoreCase);
            foreach (var word in words)
            {
                if (bad.Contains(word))
                {
                    return new ValidationResult("Описание содержит запрещенные слова", new[] { nameof(request.Body) });
                }
            }

            return ValidationResult.Success;
        }
    }
}
