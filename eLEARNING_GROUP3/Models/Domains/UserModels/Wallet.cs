using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.Domains.UserModels
{
    public class Wallet
    {
        [Key]
        public long WalletId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;

        [Required, StringLength(3)]
        public string CurrencyCode { get; set; } = "VND";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual User User { get; set; } = null!;
    }
}
