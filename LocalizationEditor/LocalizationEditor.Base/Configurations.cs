using LocalizationEditor.Base.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.Base
{
  public static class Configurations
  {
    public static void AddBase(this IServiceCollection services)
    {
      services.AddScoped<Coder>();
      services.AddScoped<IDbCoder, DbCoder>();
    }
  }
}