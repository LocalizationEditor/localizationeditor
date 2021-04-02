using System.Threading.Tasks;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Syncronize.Models;

namespace LocalizationEditor.Syncronize.Service
{
  public interface IDiffService
  {
    Task<ILocalizationDiffDto> GetDiffAsync(string sourceConnection, string destinationConnection);
    Task<LocalizationDiff> GetDiffAsync(IConnection source, IConnection destination, IUser user);
  }
}
