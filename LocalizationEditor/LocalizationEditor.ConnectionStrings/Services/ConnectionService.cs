using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Options;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class ConnectionService : IConnectionService
  {
    private readonly IPathOptionsProvider _pathOptionsProvider;

    public ConnectionService(IPathOptionsProvider provider)
    {
      _pathOptionsProvider = provider;
    }

    public async Task SaveConnectionAsync(List<IConnection> connections)
    {
      if (File.Exists(_pathOptionsProvider.FileName))
      {
        var json = await File.ReadAllTextAsync(_pathOptionsProvider.FileName);
        var existConnections = JsonConvert.DeserializeObject<List<ConnectionDto>>(json);
        connections.AddRange(existConnections);
      }
      
      var data = JsonConvert.SerializeObject(connections);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, data);
    }

    public async Task<IReadOnlyCollection<IConnection>> GetConnectionsAsync()
    {
      if (!File.Exists(_pathOptionsProvider.FileName))
        return null;

      return await GetConnectionsFromFileAsync();
    }

    public async Task<IConnection> GetConnectionByNameAsync(string name)
    {
      if (!File.Exists(_pathOptionsProvider.FileName))
        return null;

      var connections = await GetConnectionsFromFileAsync();
      return connections
        .FirstOrDefault(item => item.ConnectionName == name);
    }

    private async Task<IReadOnlyCollection<IConnection>> GetConnectionsFromFileAsync()
    {
      var json = await File.ReadAllTextAsync(_pathOptionsProvider.FileName);
      return JsonConvert.DeserializeObject<List<ConnectionDto>>(json);
    }
  }
}