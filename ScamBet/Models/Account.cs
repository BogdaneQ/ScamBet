using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ScamBet.Entitties
{
    public class Account
    {
        [Key]
        [Required]
        public int user_ID {  get; set; }

        [Required(ErrorMessage = "Enter your Usermane")]
        [StringLength(25)]
        public string Username { get; set; }
        
        [Required(ErrorMessage ="Enter your name")]
        [StringLength(25)]
        public string name { get; set; }
        
        [Required(ErrorMessage ="Enter your Surname")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage ="Please enter your password")]
        [PasswordPropertyText]
        public string password { get; set; }

        [Required(ErrorMessage = "Please enter your email")] 
        [EmailAddress(ErrorMessage ="Invalid email format")]
        public string email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string phone_number { get; set; }
        
        [Required] 
        public bool isBanned { get; set; } = false;
    }
}
