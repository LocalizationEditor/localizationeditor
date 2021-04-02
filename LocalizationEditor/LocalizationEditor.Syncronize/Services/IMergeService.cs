using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.Syncronize.Service
{
  public interface IMergeService
  {
    Task MergeAsync(IConnection source, IConnection destination, IUser user, IReadOnlyCollection<long> sourceIds = null);
  }
}
