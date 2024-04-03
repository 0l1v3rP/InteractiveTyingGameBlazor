using Microsoft.Identity.Client;

namespace InteractiveTyingGameBlazor.Models
{
    public class PublicMessage(string text, string user, string color)
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public string User { get; set; } = user;

        public string Text { get; set; } = text;

        public string Color { get; set; } = color;

        public override string ToString()
            => $"{User} [{TimeStamp}]: {Text}";
    }
}
