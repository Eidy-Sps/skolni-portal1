using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát emailu.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Pamatovat si mě?")]
        public bool RememberMe { get; set; }
    }
}