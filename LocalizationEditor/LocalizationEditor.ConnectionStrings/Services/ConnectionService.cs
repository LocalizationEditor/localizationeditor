using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.Base.Encrypt;
using Newtonsoft.Json;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Options;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class ConnectionService : IConnectionService
  {
    private readonly IPathOptionsProvider _pathOptionsProvider;
    private readonly IEncryptService _encryptService;

    public ConnectionService(IPathOptionsProvider provider, IEncryptService encryptService)
    {
      _pathOptionsProvider = provider;
      _encryptService = encryptService;
    }

    public async Task<IConnection> SaveConnectionAsync(IConnection connection)
    {
      var existConnections = new List<IConnection>(await GetConnectionsAsync());
      existConnections.Add(connection);
      var data = JsonConvert.SerializeObject(existConnections);
      var encryptData = _encryptService.Encrypt(data);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, encryptData);
      return connection;
    }

    public async Task UpdateConnection(Guid id, IConnection connection)
    {
      var existConnections = new List<IConnection>(await GetConnectionsAsync());

      var entity = existConnections.FirstOrDefault(item => item.Id == id);
      entity?.Update(connection);
      var data = JsonConvert.SerializeObject(existConnections);
      var encryptData = _encryptService.Encrypt(data);

      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, encryptData);
    }

    public async Task<IEnumerable<IConnection>> GetConnectionsAsync()
    {
      if (!File.Exists(_pathOptionsProvider.FileName))
        return Enumerable.Empty<IConnection>();

      var json = await File.ReadAllTextAsync(_pathOptionsProvider.FileName);
      var decryptData = _encryptService.Decrypt(json);
      return JsonConvert.DeserializeObject<List<ConnectionDto>>(decryptData);
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
      var encryptData = _encryptService.Encrypt(data);
      await File.WriteAllTextAsync(_pathOptionsProvider.FileName, encryptData);
    }

    public async Task<IConnection> GetConnectionByIdAsync(Guid id)
    {
      var connections = await GetConnectionsAsync();
      return connections
        .SingleOrDefault(item => item.Id == id);
    }
  }
}