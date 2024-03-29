using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Models;
using System.Threading.Tasks;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionStringResolverService
  {
    Task<string> GetConnectionStringAsync(string connectionKey, IUser user);
    string GetConnectionString(IConnection connection);
  }
}