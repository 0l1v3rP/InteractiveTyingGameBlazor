using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.DbModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
