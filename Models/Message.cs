using InteractiveTyingGameBlazor.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveTyingGameBlazor.Models
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; }
        public string RecipentId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Content { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }

        [ForeignKey("RecipentId")]
        public virtual ApplicationUser Recipent { get; set; }
    }
}
