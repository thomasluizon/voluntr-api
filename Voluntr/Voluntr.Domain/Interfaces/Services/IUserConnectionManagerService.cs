﻿namespace Voluntr.Domain.Interfaces.Services
{
    public interface IUserConnectionManagerService
    {
        void KeepUserConnection(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string userId);
    }
}
