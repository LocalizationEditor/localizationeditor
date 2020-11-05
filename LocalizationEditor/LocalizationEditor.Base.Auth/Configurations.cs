using System;
using LocalizationEditor.Base.Auth.Infrastructure;
using LocalizationEditor.Base.Auth.Options;
using LocalizationEditor.Base.Auth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.Base.Auth
{
  public static class Configurations
  {
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
      var section = configuration.GetSection("AuthOptions");
      services.Configure<AuthOptions>(i =>
      {
        i.Audience = section["Audience"];
        i.Issuer = section["Issuer"];
        i.Secret = section["Secret"];
        i.TokenLifeTime = Convert.ToInt32(section["TokenLifeTime"]);
      });
      services.AddTransient<IAuthOptionsProvider, AuthOptionsProvider>();

      services.AddAuthServices();
    }

    private static void AddAuthServices(this IServiceCollection services)
    {
      services.AddTransient<IAuthService, AuthService>();
    }
  }
}