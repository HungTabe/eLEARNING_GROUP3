using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace OnlineLearning.Models.Domains.UserModels
{
    public class UserRole
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
