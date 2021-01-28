using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.Syncronize.Service
{
  public interface IMergeService
  {
    Task Merge(IConnection source, IConnection destination);
  }
}
