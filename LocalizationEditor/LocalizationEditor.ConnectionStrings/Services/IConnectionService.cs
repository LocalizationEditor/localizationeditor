using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionService
  {
    Task<IConnection> SaveConnectionAsync(IConnection connections, IUser user);
    Task<IEnumerable<IConnection>> GetConnectionsAsync(IUser user);
    Task<IConnection> GetConnectionByNameAsync(string name, IUser user);
    Task<IConnection> GetConnectionByIdAsync(Guid id, IUser user);
    Task UpdateConnection(Guid id, IConnection connection, IUser user);
    Task Remove(Guid id, IUser user);
  }
}