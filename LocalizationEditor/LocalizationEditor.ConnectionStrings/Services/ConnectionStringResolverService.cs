using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class ConnectionStringResolverService
    : IConnectionStringResolverService
  {
    private readonly IConnectionService _connectionService;
    private readonly IEnumerable<IDataBaseConnectionResolver> _resolvers;

    public ConnectionStringResolverService(IConnectionService connectionService,
      IEnumerable<IDataBaseConnectionResolver> resolvers)
    {
      _connectionService = connectionService;
      _resolvers = resolvers;
    }

    private IDataBaseConnectionResolver GetConnectionResolver(IConnection connection)
    {
      return _resolvers.First(i => i.CanHandle(connection));
    }

    public async Task<string> GetConnectionStringAsync(string connectionKey, IUser user)
    {
      var connection = await _connectionService.GetConnectionByNameAsync(connectionKey, user);
      return GetConnectionString(connection);
    }

    public string GetConnectionString(IConnection connection)
    {
      return GetConnectionResolver(connection).GetConnectionString(connection);
    }
  }
}