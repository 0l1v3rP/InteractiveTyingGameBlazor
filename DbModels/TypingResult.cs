using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class TypingResult : BaseEntity
    {
        [AllowNull]
        public string? UserId { get; set; } = null;

		[Required]
		public string VideoId { get; set; }

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

        private ICollection<TypedChar>? _charsCollection;

        [NotMapped]
        public ICollection<TypedChar>? CharsCollection
        {
            get
            {
                if (_charsCollection == null && !string.IsNullOrEmpty(Chars))
                {
                    _charsCollection = JsonSerializer.Deserialize<ICollection<TypedChar>>(Chars);
                }
                return _charsCollection;
            }
            set
            {
                _charsCollection = value;
                Chars = JsonSerializer.Serialize(_charsCollection);
            }
        }
    }
}