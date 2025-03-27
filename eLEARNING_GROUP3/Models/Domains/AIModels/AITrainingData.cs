using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.Domains.AIModels
{
    public class AITrainingData
    {
        [Key]
        public long DataId { get; set; }
        [Required]
        public string InputText { get; set; } = null!;
        [Required]
        public string ExpectedOutput { get; set; } = null!;
        public float[]? VectorEmbedding { get; set; } // vector embedding
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
