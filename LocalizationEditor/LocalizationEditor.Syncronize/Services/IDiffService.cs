using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Syncronize.Models;

namespace LocalizationEditor.Syncronize.Service
{
  public interface IDiffService
  {
    ILocalizationDiffDto GetDiff(string source, string destination);
    Task<LocalizationDiff> GetDiffAsync(IConnection source, IConnection destination);
  }
}
