namespace InteractiveTyingGameBlazor.Chat
{
    public class CookieStorage
    {
        private readonly Dictionary<string, Dictionary<string, string>> _cookies = [];

        public void Add(string userId, Dictionary<string, string> cookies)
            => _cookies[userId] = cookies;

        public void Remove(string userId)
            => _cookies.Remove(userId);

        public Dictionary<string, string>? GetUserCookies(string userId)
            => _cookies.GetValueOrDefault(userId);
    }
}