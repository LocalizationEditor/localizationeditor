using Auth.Application.Infrastructure.Repository;
using Auth.Persistence.Context;
using Auth.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence
{
  public static class Configurations
  {
    public static void AddAuthPersistence(this IServiceCollection services)
    {
      services.AddTransient<AuthContext>();
      services.AddTransient<IAuthRepository, AuthRepository>();
    }
  }
}