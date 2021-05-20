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
    /// <summary>
    /// Find all available for <paramref name="user"/> connections and group by file name
    /// </summary>
    /// <param name="user"><see cref="IUser"/></param>
    /// <returns>Dictionary - key = fileName; value = list of <see cref="IConnection"/></returns>
    Task<IDictionary<string, List<IConnection>>> GetConnectionsMapAsync(IUser user);
  }
}