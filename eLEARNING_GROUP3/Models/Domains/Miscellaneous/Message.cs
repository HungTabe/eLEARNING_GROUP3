using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.Miscellaneous
{
    public class Message
    {
        [Key]
        public long MessageId { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        [Required]
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;

        public virtual User Sender { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
    }
}
