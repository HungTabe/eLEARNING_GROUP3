using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.Miscellaneous
{
    public class TransactionHistory
    {
        [Key]
        public long TransactionId { get; set; }

        public long UserId { get; set; }

        public long? WalletId { get; set; }

        public long? CourseId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [StringLength(100)]
        public string? ExternalTransactionId { get; set; }

        [Required]
        public TransactionStatus Status { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Wallet? Wallet { get; set; }
        public virtual Course? Course { get; set; }
    }
}
