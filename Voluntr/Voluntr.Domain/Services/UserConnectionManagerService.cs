using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.Services
{
    public class UserConnectionManagerService : IUserConnectionManagerService
    {
        private static readonly Dictionary<string, List<string>> userConnectionMap = [];
        private static readonly string userConnectionMapLocker = string.Empty;

        public List<string> GetUserConnections(string userId)
        {
            var conn = new List<string>();

            lock (userConnectionMapLocker)
            {
                if (userConnectionMap.TryGetValue(userId, out List<string> value))
                    conn = value;
            }

            return conn;
        }

        public void KeepUserConnection(string userId, string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                if (!userConnectionMap.TryGetValue(userId, out List<string> value))
                {
                    value = [];
                    userConnectionMap[userId] = value;
                }

                value.Add(connectionId);
            }
        }

        public void RemoveUserConnection(string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                foreach (var userId in userConnectionMap.Keys)
                {
                    if (userConnectionMap.TryGetValue(userId, out List<string> value))
                    {
                        value.Remove(connectionId);
                        break;
                    }
                }
            }
        }
    }
}
