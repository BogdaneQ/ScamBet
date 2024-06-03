using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using ScamBet.Models;

namespace ScamBet.Entities
{
    public class Account
    {
        [Key]
        [Required]
        public int user_ID { get; set; }

        [ForeignKey("RoleID")]
        [Required]
        public int role_ID { get; set; }
        public virtual Role Role { get; set; }

        [Required(ErrorMessage = "Enter your Usermane")]
        [StringLength(25)]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter your Name")]
        [StringLength(25)]
        public string name { get; set; }

        [Required(ErrorMessage = "Enter your Surname")]
        [StringLength(50)]
        public string surname { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [PasswordPropertyText]
        public string password { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string phone_number { get; set; }

        [Required]
        public bool isBanned { get; set; } = false;

        [Required]
        [Range(0, double.MaxValue)]
        public double acc_balance { get; set; }
    }
}
