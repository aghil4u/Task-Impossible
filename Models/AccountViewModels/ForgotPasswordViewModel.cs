using System.ComponentModel.DataAnnotations;

namespace TaskImpossible.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}