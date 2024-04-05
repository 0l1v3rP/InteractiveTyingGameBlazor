using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class RegisteredVideo : BaseEntity
    {
        public string UserId { get; set; }
        [Required]

        public string URL { get; set; }
        [Required]

        public string Language { get; set; }
        [Required]

        public CategoryType? Category { get; set; } = null;
        [Required]

        public string Title { get; set; }

        /// <summary>
        /// Specifies how many times was this video played
        /// </summary>
        [Required]

        public int Counter { get; set; } = 0;
        [Required]

        public bool IsGlobal { get; set; } = false;

		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; }
    }
}
