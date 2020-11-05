using System.Reflection;
using Auth.Application.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application
{
  public static class Configurations
  {
    public static void AddAuthApplication(this IServiceCollection services)
    {
      services.AddMediatR(Assembly.GetExecutingAssembly());
      services.AddTransient<IUserAuthMapper, UserAuthMapper>();
    }
  }
}