using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionService
  {
    Task SaveConnectionAsync(IConnection connections);
    Task<IEnumerable<IConnection>> GetConnectionsAsync();
    Task<IConnection> GetConnectionByNameAsync(string name);
    Task UpdateConnection(long id, IConnection connection);
    Task Remove(long id);
  }
}