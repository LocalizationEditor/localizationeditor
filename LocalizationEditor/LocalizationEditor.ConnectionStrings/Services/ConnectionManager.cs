using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;
using LocalizationEditor.ConnectionStrings.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class ConnectionManager : IConnectionManager
  {
    private readonly IConnectionService _connectionService;

    public ConnectionManager(IConnectionService connectionService)
    {
      _connectionService = connectionService;
    }

    public async Task<IEnumerable<IConnection>> GetConnectionsAsync(IUser user)
    {
      return await _connectionService.GetConnectionsAsync(user);
    }

    public async Task Remove(Guid id, IUser user)
    {
      ValidateAccess(user);
      await _connectionService.Remove(id, user);
    }

    public async Task<IConnection> SaveConnectionAsync(IConnection connections, IUser user)
    {
      ValidateAccess(user);
      return await _connectionService.SaveConnectionAsync(connections, user);
    }

    public async Task UpdateConnection(Guid id, IConnection connection, IUser user)
    {
      ValidateAccess(user);
      await _connectionService.UpdateConnection(id, connection, user);
    }

    private void ValidateAccess(IUser user)
    {
      if (user.Role == RoleType.User)
        throw new InvalidOperationException("Access denied");
    }
  }
}
