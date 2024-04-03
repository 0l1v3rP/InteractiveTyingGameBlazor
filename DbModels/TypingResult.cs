using InteractiveTyingGameBlazor.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class TypingResult : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        [AllowNull]
        public string? UserId { get; set; } = null;

        [AllowNull]
        public string? SessionId { get; set; } = null;

        [Required]
        public int CPM { get; set; }

        [Required]
        public float Accuracy { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string VideoId { get; set; }

        public ApplicationUser? User { get; set; }

        public string Chars { get; set; }
    }
}
