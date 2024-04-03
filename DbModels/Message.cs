using InteractiveTyingGameBlazor.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class Message : BaseEntity
    {
        [Required]

        public string SenderId { get; set; }
        [Required]

        public string RecipentId { get; set; }
        [Required]

        public DateTime Timestamp { get; set; } = DateTime.Now;
        [Required]

        public string Content { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }

        [ForeignKey("RecipentId")]
        public virtual ApplicationUser Recipent { get; set; }
    }
}
