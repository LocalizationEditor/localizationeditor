using System;

namespace LocalizationEditor.Admin.Models.Implementations
{
  internal class Role : IRole
  {
    public Role( Guid userId, RoleType roleType)
    {
      UserId = userId;
      RoleType = roleType;
    }

    public Guid UserId { get; }
    public RoleType RoleType { get; }
  }
}
