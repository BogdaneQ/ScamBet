using System.ComponentModel.DataAnnotations;

namespace ScamBet.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email użytkownika jest wymagany.")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
