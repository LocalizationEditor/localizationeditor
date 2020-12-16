using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace LocalizationEditor.Base.Extensions
{
  public static class AssemblyHelper
  {
    public static IReadOnlyCollection<Assembly> GetAssemblies(
      this Assembly assembly, string rootContentPath = "LocalizationEditor")
    {
      var assemblies = new List<Assembly>();
      GetDependentAssemblies(assembly, assemblies, rootContentPath);
      assemblies.Add(assembly);
      return assemblies;
    }

    public static void AddDiForDependentAssemblies(this Assembly assembly, 
      ContainerBuilder builder,
      IConfiguration configuration)
    {
      var assemblies = assembly.GetAssemblies().ToArray();
      builder.RegisterAssemblyModules(assemblies);
    }

    private static void GetDependentAssemblies(
      Assembly assembly, ICollection<Assembly> dependentAssemblyList, string rootContentPath)
    {
      var referencedAssemblies = assembly.GetReferencedAssemblies()
        .Where(i => i.FullName.StartsWith(rootContentPath));

      foreach (var referencedAssembly in referencedAssemblies)
      {
        if (dependentAssemblyList.Any(i => i.FullName == referencedAssembly.FullName))
          continue;
        var loadedAssembly = Assembly.Load(referencedAssembly.FullName);
        dependentAssemblyList.Add(loadedAssembly);
        GetDependentAssemblies(loadedAssembly, dependentAssemblyList, rootContentPath);
      }
    }
  }
}