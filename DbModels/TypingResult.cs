using InteractiveTyingGameBlazor.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class TypingResult : BaseEntity
    {
        [AllowNull]
        public string? UserId { get; set; } = null;

		[Required]
		public Guid VideoId { get; set; }

		[AllowNull]
        public string? SessionId { get; set; } = null;

        [Required]
        public int CPM { get; set; }

        [Required]
        public float Accuracy { get; set; }

        [Required]
        public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
		
        [ForeignKey("VideoId")]
		public virtual RegisteredVideo Video { get; set; }  

        public string Chars { get; set; }
    }
}
