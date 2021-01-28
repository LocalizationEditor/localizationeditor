using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.Syncronize.Service
{
  internal class DiffService : IDiffService
  {
    public Task<object> GetDiff(IConnection source, IConnection destination)
    {
      throw new System.NotImplementedException();
    }
  }
}