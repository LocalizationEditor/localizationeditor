using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionService
  {
    Task SaveConnectionAsync(IConnection connections);
    Task<IReadOnlyCollection<IConnection>> GetConnectionsAsync();
    Task<IConnection> GetConnectionByNameAsync(string name);
    Task Remove(long id);
  }
}