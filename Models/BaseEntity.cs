using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
