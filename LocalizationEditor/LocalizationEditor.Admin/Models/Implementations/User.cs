using Newtonsoft.Json;
using System;

namespace LocalizationEditor.Admin.Models.Implementations
{
  public class User : IUser
  {
    [JsonConstructor]
    public User(Guid id,
      string email,
      string password,
      RoleType role)
    {
      Id = id == Guid.Empty ? Guid.NewGuid() : id;
      Email = email;
      Password = password;
      Role = role;
    }

    public Guid Id { get; }
    public string Email { get; }
    public string Password { get; }
    public RoleType Role { get; private set; }

    public void Update(IUser user)
    {
      Role = user.Role;
    }
  }
}
