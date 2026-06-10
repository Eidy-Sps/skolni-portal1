using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress]
        [Display(Name = "Školní Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Pamatovat si mě?")]
        public bool RememberMe { get; set; }
    }
}