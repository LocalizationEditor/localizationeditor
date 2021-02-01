using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionService
  {
    Task<IConnection> SaveConnectionAsync(IConnection connections);
    Task<IEnumerable<IConnection>> GetConnectionsAsync();
    Task<IConnection> GetConnectionByNameAsync(string name);
    Task<IConnection> GetConnectionByIdAsync(Guid id);
    Task UpdateConnection(Guid id, IConnection connection);
    Task Remove(Guid id);
  }
}