using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress(ErrorMessage = "Zadejte platný email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Heslo je povinné")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Zůstat přihlášen")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
