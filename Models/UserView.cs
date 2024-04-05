using InteractiveTyingGameBlazor.Data.Services;
using SQLitePCL;

namespace InteractiveTyingGameBlazor.Models
{
    public class UserView(string username, string email, DateOnly? lastGame, int totalPlayed)
    {
        public string UserName { get; set; } = username;

        public string Email { get; set; } = email;

        public DateOnly? LastGame { get; set; } = lastGame;

        public int TotalPlayed { get; set; } = totalPlayed;
    }
}
