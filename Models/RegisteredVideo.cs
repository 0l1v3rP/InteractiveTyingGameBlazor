using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class RegisteredVideo : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        [AllowNull]
        public string UserId { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public CategoryType? Category { get; set; } = null;

        [Required]
        public string Title { get; set; }

        [Required]
        public bool IsGlobal { get; set; } = false;
        public ApplicationUser User { get; set; }
    }
}
