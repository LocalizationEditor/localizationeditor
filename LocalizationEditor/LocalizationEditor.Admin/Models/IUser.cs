using LocalizationEditor.Admin.Models.Implementations;
using System;

namespace LocalizationEditor.Admin.Models
{
  public interface IUser
  {
    string Email { get; }
    Guid Id { get; }
    string Password { get; }
    RoleType Role { get; }

    void Update(IUser user);
  }
}