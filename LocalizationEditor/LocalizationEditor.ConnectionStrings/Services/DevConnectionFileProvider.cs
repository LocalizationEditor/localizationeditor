using System.Collections.Generic;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;
using IPathOptionsProvider = LocalizationEditor.ConnectionStrings.Options.IPathOptionsProvider;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class DevConnectionFileProvider : IConnectionFileProvider
  {
    private readonly IPathOptionsProvider _pathOptionsProvider;

    public bool CanHandle(RoleType roleType) => roleType == RoleType.Dev || roleType == RoleType.Admin;

    public DevConnectionFileProvider(IPathOptionsProvider pathOptionsProvider)
    {
      _pathOptionsProvider = pathOptionsProvider;
    }

    public HashSet<string> GetFilesPath(IUser user)
    {
      return new HashSet<string> { _pathOptionsProvider.FileName, user.Id.ToString() };
    }
  }
}