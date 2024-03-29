using System.Configuration;

namespace InteractiveTyingGameBlazor.Models
{
    public class TypedChar(int second, char character, bool isCorrect)
    {
        public int Second { get; set; } = second;

        public char Character { get; set; } = character;

        public bool IsCorrect { get; set; } = isCorrect;
    }
}
