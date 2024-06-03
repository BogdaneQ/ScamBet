using ScamBet.Entities;
using System.ComponentModel.DataAnnotations;

namespace ScamBet.Models
{
    public enum RoleType
    {
        User = 1,
        Admin = 2
    }
    public class Role
    {
        [Key]
        public int role_ID { get; set; }

        public string RoleName { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
