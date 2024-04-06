using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.DbModels
{
    public interface IEntityWithId
    {
        string Id { get; set; }
    }
    public class BaseEntity : IEntityWithId
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
