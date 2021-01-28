using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.Syncronize.Service
{
  public interface IDiffService
  {
    Task<object> GetDiff(IConnection source, IConnection destination);
  }
}
