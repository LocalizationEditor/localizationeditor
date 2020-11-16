using ConnectionString.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectionString
{
  public static class Configuration
  {
    public static void AddConnectionString(this IServiceCollection service)
    {
      service.AddTransient<IConnectionProvider, ConnectionProvider>();
    }
  }
}