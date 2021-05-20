using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.Base.Encrypt;
using Newtonsoft.Json;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;
using IPathOptionsProvider = LocalizationEditor.ConnectionStrings.Options.IPathOptionsProvider;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class ConnectionService : IConnectionService
  {
    private readonly IPathOptionsProvider _pathOptionsProvider;
    private readonly IEncryptService _encryptService;
    private readonly IEnumerable<IConnectionFileProvider> _connectionFileProviders;

    public ConnectionService(IPathOptionsProvider provider, IEncryptService encryptService, IEnumerable<IConnectionFileProvider> connectionFileProviders)
    {
      _pathOptionsProvider = provider;
      _encryptService = encryptService;
      _connectionFileProviders = connectionFileProviders;
    }

    public async Task<IConnection> SaveConnectionAsync(IConnection connections, IUser user)
    {
      if (connections.ForAll && user.Role == RoleType.Admin)
        return await SaveAsync(connections, _pathOptionsProvider.FileName);

      return await SaveAsync(connections, user.Id.ToString());
    }

    private async Task<IConnection> SaveAsync(IConnection connection, string file)
    {
      var existConnections = new List<IConnection>(await GetConnectionsAsync(file))
        {
          connection
        };
      var data = JsonConvert.SerializeObject(existConnections);
      var encryptData = _encryptService.Encrypt(data);

      await File.WriteAllTextAsync(file, encryptData);
      return connection;
    }

    public async Task UpdateConnection(Guid id, IConnection connection, IUser user)
    {
      var file = await GetFilePathByConnectionId(id, user);
      var existConnections = await GetConnectionsAsync(file);
      var entity = existConnections.FirstOrDefault(i => i.Id == id);
      entity?.Update(connection);

      await SaveToFile(existConnections.Where(i => i.Id != id), file);

      if (entity?.ForAll == true)
      {
        var allConnections = await GetConnectionsAsync(_pathOptionsProvider.FileName);
        await SaveToFile(allConnections.Append(entity), _pathOptionsProvider.FileName);
      }
      else
      {
        var allConnections = await GetConnectionsAsync(user.Id.ToString());
        await SaveToFile(allConnections.Append(entity), user.Id.ToString());
      }
    }

    public async Task<IEnumerable<IConnection>> GetConnectionsAsync(IUser user)
    {
      var files = GetFilesPath(user);
      var connections = new List<IConnection>();
      foreach (var file in files)
        connections.AddRange(await GetConnectionsAsync(file));

      return connections;
    }

    public async Task<IDictionary<string, List<IConnection>>> GetConnectionsMapAsync(IUser user)
    {
      var files = GetFilesPath(user);
      var connectionsMap = new Dictionary<string, List<IConnection>>();
      foreach (var file in files)
      {
        var connections = await GetConnectionsAsync(file);
        connectionsMap.Add(file, connections.ToList());
      }

      return connectionsMap;
    }

    private async Task SaveToFile(IEnumerable<IConnection> connections, string file)
    {
      var data = JsonConvert.SerializeObject(connections);
      var encryptData = _encryptService.Encrypt(data);

      await File.WriteAllTextAsync(file, encryptData);
    }

    private async Task<string> GetFilePathByConnectionId(Guid id, IUser user)
    {
      var files = GetFilesPath(user);
      foreach (var file in files)
      {
        var filesByPath = await GetConnectionsAsync(file);
        var foundFile = filesByPath.FirstOrDefault(i => i?.Id == id);
        if (foundFile != null)
        {
          return file;
        }
      }

      return null;
    }

    private async Task<IEnumerable<IConnection>> GetConnectionsAsync(string path)
    {
      if (!File.Exists(path))
        return Enumerable.Empty<IConnection>();

      var json = await File.ReadAllTextAsync(path);
      var decryptData = _encryptService.Decrypt(json);

      return JsonConvert.DeserializeObject<List<ConnectionDto>>(decryptData);
    }

    public async Task<IConnection> GetConnectionByNameAsync(string name, IUser user)
    {
      if (!File.Exists(_pathOptionsProvider.FileName))
        return null;

      var connections = await GetConnectionsAsync(user);
      return connections
        .FirstOrDefault(item => item.ConnectionName == name);
    }

    public async Task Remove(Guid id, IUser user)
    {
      var connections = await GetConnectionsMapAsync(user);
      foreach(var kvp in connections.Where(i => i.Value.Any(j => j.Id == id)))
      {
        var removeEntity = connections[kvp.Key].First(i => i.Id == id);

        kvp.Value.Remove(removeEntity);

        var json = JsonConvert.SerializeObject(kvp.Value);
        var encryptData = _encryptService.Encrypt(json);
        await File.WriteAllTextAsync(kvp.Key, encryptData);
      }
    }

    public async Task<IConnection> GetConnectionByIdAsync(Guid id, IUser user)
    {
      var connections = await GetConnectionsAsync(user);
      return connections
        .SingleOrDefault(item => item.Id == id);
    }

    public HashSet<string> GetFilesPath(IUser user)
    {
      return _connectionFileProviders.FirstOrDefault(i => i.CanHandle(user.Role))?.GetFilesPath(user);
    }
  }
}