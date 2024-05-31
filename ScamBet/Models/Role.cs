using System.ComponentModel.DataAnnotations;

namespace ScamBet.Models
{
    public enum RoleType
    {
        User = 0,
        Admin = 1
    }

    public class Role
    {
        [Key]
        public int role_ID { get; set; }

        public RoleType roleName { get; set; }
    }
}
