using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.Miscellaneous
{
    public class Certificate
    {
        [Key]
        public long CertificateId { get; set; }
        public long CourseId { get; set; }
        public long UserId { get; set; }
        public long IssuerId { get; set; }
        public DateTime? IssueDate { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }
        public virtual User? Issuer { get; set; }
    }
}
