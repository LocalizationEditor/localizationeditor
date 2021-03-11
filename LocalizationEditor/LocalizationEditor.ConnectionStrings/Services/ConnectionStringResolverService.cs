using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<string> GetConnectionStringAsync(string connectionKey)
    {
      var connection = await _connectionService.GetConnectionByNameAsync(connectionKey);
      return GetConnectionResolver(connection).GetConnectionString(connection);
    }
  }
}