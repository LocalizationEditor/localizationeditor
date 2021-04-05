using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionManager
  {
    Task<IConnection> SaveConnectionAsync(IConnection connections, IUser user);
    Task<IEnumerable<IConnection>> GetConnectionsAsync(IUser user);
    Task UpdateConnection(Guid id, IConnection connection, IUser user);
    Task Remove(Guid id, IUser user);
  }
}
