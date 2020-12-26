using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.Base.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddOptions(
      this IServiceCollection services, 
      IConfiguration configuration,
      Assembly rootAssembly)
    {
      var assemblies = rootAssembly.GetAssemblies();
      var types = assemblies
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsAssignableTo<IConfigurationModule>() && !type.IsInterface);

      foreach (var type in types)
      {
        if (Activator.CreateInstance(type) is IConfigurationModule module)
        {
          module.ConfigurationRoot = configuration;
          module.AddOptions(services);
        }
      }
    }
  }
}