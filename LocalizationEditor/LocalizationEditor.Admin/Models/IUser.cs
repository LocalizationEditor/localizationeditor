using System;

namespace LocalizationEditor.Admin.Models
{
  public interface IUser
  {
    string Email { get; }
    Guid Id { get; }
    string Password { get; }
    IRole Role { get; }

    void Update(IUser user);
  }
}