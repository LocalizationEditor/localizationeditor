using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.Merge.Service
{
  public interface IMergeService
  {
    Task Merge(IConnection source, IConnection destination);
  }
}