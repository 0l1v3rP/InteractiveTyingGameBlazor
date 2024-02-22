using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.Models
{
    using InteractiveTyingGameBlazor.Data;
    using InteractiveTyingGameBlazor.DbModels;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public class TypingResult : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        [AllowNull]
        public string UserId { get; set; }

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

        [NotMapped]
        public List<MissTypedChar> MissTypedChars { get; set; }
    }
}
