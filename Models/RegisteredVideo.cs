using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveTyingGameBlazor.Models
{
    public class RegisteredVideo : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public string URL { get; set; }

        public string Language { get; set; }

        public CategoryType? Category { get; set; } = null;

        public string Title { get; set; }

        /// <summary>
        /// Specifies how many times was this video played
        /// </summary>
        public int Counter { get; set; } = 0;
        public bool IsGlobal { get; set; } = false;
        public ApplicationUser User { get; set; }
    }
}
