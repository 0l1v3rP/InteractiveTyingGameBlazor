using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace InteractiveTyingGameBlazor.Chat
{
    public class ConnectionService
    {
        private readonly Dictionary<string, HashSet<string>> groupConnections = [];
        public void AddConnection(string user, string connectionId)
        {
            lock (groupConnections)
            {
                if (!groupConnections.ContainsKey(user))
                    groupConnections[user] = [];

                groupConnections[user].Add(connectionId);
            }
        }

        public void RemoveConnection(string user, string connectionId)
        {
            lock (groupConnections)
            {
                if (groupConnections.ContainsKey(user))
                {
                    groupConnections[user].Remove(connectionId);

                    if (groupConnections[user].Count == 0)
                        groupConnections.Remove(user);
                }
            }
        }

        public HashSet<string> GetUserConnections(string user)
        {
            lock (groupConnections)
                return groupConnections.TryGetValue(user, out var connections) ? connections : new HashSet<string>();
        }
        public bool AnyUserConnection(string user)
        {
            lock (groupConnections)
                return groupConnections.TryGetValue(user, out var connections);
        }
    }
}
