using LocalizationEditor.Admin.Models.Implementations;
using System;

namespace LocalizationEditor.Admin.Models
{
  public interface IRole
  {
    RoleType RoleType { get; }
    Guid UserId { get; }
  }
}