using System.Collections.Generic;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionFileProvider
  {
    bool CanHandle(RoleType roleType);
    HashSet<string> GetFilesPath(IUser user);
  }
}