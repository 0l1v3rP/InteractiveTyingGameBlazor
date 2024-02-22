using InteractiveTyingGameBlazor.DbModels;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.Models
{
    public class MissTypedChar(int second, char missChar) : BaseEntity
    {
        public int Second { get; set; } = second;
        public char MissChar { get; set; } = missChar;
    }
}