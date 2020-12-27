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

    public async Task SaveConnectionAsync(IConnection connection)
    {
      var existConnections = new List<IConnection>();
      if (File.Exists(_pathOptionsProvider.FileName))
      {
        var json = await File.ReadAllTextAsync(_pathOptionsProvider.FileName);
        existConnections.AddRange(JsonConvert.DeserializeObject<List<ConnectionDto>>(json));
        if (IsNewEntity(connection))
        {
          connection.UpdateId(GenerateNewId(existConnections));
          existConnections.Add(connection);
        }
        else
        {
          var entity = existConnections.FirstOrDefault(item => item.Id == connection.Id);
          entity.Update(
            connection.ConnectionName,
            connection.Server,
            connection.DbName,
            connection.UserName,
            connection.Password,
            connection.DataBaseType);
        }
      }


      var data = JsonConvert.SerializeObject(existConnections);
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

    public async Task Remove(long id)
    {
      var connections = await GetConnectionsAsync();
      var removeEntity = connections
        .FirstOrDefault(item => item.Id == id);

      var list = connections.ToList();
      list.Remove(removeEntity);
      var data = JsonConvert.SerializeObject(list);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, data);
    }

    private async Task<IReadOnlyCollection<IConnection>> GetConnectionsFromFileAsync()
    {
      var json = await File.ReadAllTextAsync(_pathOptionsProvider.FileName);
      return JsonConvert.DeserializeObject<List<ConnectionDto>>(json);
    }

    private bool IsNewEntity(IConnection connection)
    {
      return connection.Id == 0;
    }

    private long GenerateNewId(IReadOnlyCollection<IConnection> connections)
    {
      return connections.Count != 0
        ? connections.Max(item => item.Id) + 1
        : 1;
    }
  }
}