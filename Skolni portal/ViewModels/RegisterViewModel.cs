using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Jméno je povinné")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Příjmení je povinné")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress(ErrorMessage = "Neplatný formát emailu")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@spstrutnov\.cz$", ErrorMessage = "Musíš použít školní email končící na @spstrutnov.cz!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdit heslo")]
        [Compare("Password", ErrorMessage = "Hesla se neshodují.")]
        public string ConfirmPassword { get; set; }
    }
}