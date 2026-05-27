using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Školní email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát emailu.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        [StringLength(100, ErrorMessage = "Heslo musí mít alespoň {2} znaků.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrzení hesla je povinné.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Zadaná hesla se neshodují.")]
        public string ConfirmPassword { get; set; }
    }
}