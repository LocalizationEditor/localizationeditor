using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  public interface IConnectionStringResolverService
  {
    DataBaseConnectionResolver GetConnectionResolver(IConnection connection);
  }
}