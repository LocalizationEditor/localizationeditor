using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;
using LocalizationEditor.Admin.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Admin.Services.Implementation
{
  internal class UserService : IUserService
  {
    private readonly IPathOptionsProvider _pathOptionsProvider;

    public UserService(IPathOptionsProvider pathOptionsProvider)
    {
      _pathOptionsProvider = pathOptionsProvider;
    }

    public async Task<IUser> Add(IUser user)
    {
      var users = await GetUsersAsync();
      users.Add(user);
      var data = JsonConvert.SerializeObject(users);
      await File.WriteAllTextAsync(_pathOptionsProvider.Auth, data);
      return user;
    }

    public async Task<IUser> Update(Guid id, IUser user)
    {
      var existConnections = await GetUsersAsync();

      var entity = existConnections.FirstOrDefault(item => item.Id == id);
      entity?.Update(user);
      var data = JsonConvert.SerializeObject(existConnections);
      await File.WriteAllTextAsync(_pathOptionsProvider.Auth, data);
      return user;
    }

    public async Task<Guid> Remove(Guid id)
    {
      var connections = await GetUsersAsync();
      var removeEntity = connections.FirstOrDefault(item => item.Id == id);

      connections.Remove(removeEntity);
      var data = JsonConvert.SerializeObject(connections);
      await File.WriteAllTextAsync(_pathOptionsProvider.Auth, data);
      return id;
    }

    public async Task<IEnumerable<IUser>> GetAll()
    {
      return await GetUsersAsync();
    }

    public async Task<IUser> GetByEmail(string email)
    {
      var connections = await GetUsersAsync();
      return connections
        .FirstOrDefault(item => string.Equals(item.Email, email, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task<List<IUser>> GetUsersAsync()
    {
      if (!File.Exists(_pathOptionsProvider.Auth))
        return new List<IUser>();

      var json = await File.ReadAllTextAsync(_pathOptionsProvider.Auth);
      return JsonConvert.DeserializeObject<List<User>>(json).Cast<IUser>().ToList();
    }
  }
}
