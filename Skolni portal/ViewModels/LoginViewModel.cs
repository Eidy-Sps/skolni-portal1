using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}