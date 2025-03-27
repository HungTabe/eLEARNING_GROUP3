using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Models.Domains.UserModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required, EmailAddress, StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Password { get; set; } = string.Empty;

        public DateOnly? Dob { get; set; }

        [StringLength(255)]
        public string? FullName { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsActived { get; set; } = true;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(2000)]
        public string? AvatarUrl { get; set; }

        public bool? Gender { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual Wallet? Wallet { get; set; }
        public virtual ICollection<Course> CreatedCourses { get; set; } = new List<Course>();
        public virtual ICollection<Course> AcceptedCourses { get; set; } = new List<Course>();
    }
}
