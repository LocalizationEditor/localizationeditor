using System;
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

    public async Task<IConnection> SaveConnectionAsync(IConnection connection)
    {
      var existConnections = new List<IConnection>(await GetConnectionsAsync());
      existConnections.Add(connection);
      var data = JsonConvert.SerializeObject(existConnections);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, data);
      return connection;
    }

    public async Task UpdateConnection(Guid id, IConnection connection)
    {
      var existConnections = new List<IConnection>(await GetConnectionsAsync());

      var entity = existConnections.FirstOrDefault(item => item.Id == id);
      entity?.Update(connection);

      var data = JsonConvert.SerializeObject(existConnections);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, data);
    }

    public async Task<IEnumerable<IConnection>> GetConnectionsAsync()
    {
      if (!File.Exists(_pathOptionsProvider.FileName))
        return Enumerable.Empty<IConnection>();

      var json = await File.ReadAllTextAsync(_pathOptionsProvider.FileName);
      return JsonConvert.DeserializeObject<List<ConnectionDto>>(json);
    }

    public async Task<IConnection> GetConnectionByNameAsync(string name)
    {
      if (!File.Exists(_pathOptionsProvider.FileName))
        return null;

      var connections = await GetConnectionsAsync();
      return connections
        .FirstOrDefault(item => item.ConnectionName == name);
    }

    public async Task Remove(Guid id)
    {
      var connections = await GetConnectionsAsync();
      var removeEntity = connections
        .FirstOrDefault(item => item.Id == id);

      var list = connections.ToList();
      list.Remove(removeEntity);
      var data = JsonConvert.SerializeObject(list);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, data);
    }
  }
}