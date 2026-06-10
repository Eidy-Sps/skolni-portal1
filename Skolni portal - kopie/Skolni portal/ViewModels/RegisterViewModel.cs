using System.ComponentModel.DataAnnotations;

namespace Skolni_portal.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát emailu.")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@spstrutnovska\.cz$", ErrorMessage = "Registrace je povolena pouze pro školní emaily (končící na @spstrutnovska.cz)")]
        [Display(Name = "Školní Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        [Compare("Password", ErrorMessage = "Hesla se neshodují.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Registruji se jako učitel")]
        public bool IsTeacher { get; set; } = false;

        [Display(Name = "Správní kód pro učitele")]
        public string? TeacherCode { get; set; }
    }
}