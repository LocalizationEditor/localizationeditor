using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace LocalizationEditor.Base.Extensions
{
  public static class AssemblyHelper
  {
    public static IReadOnlyCollection<Assembly> GetAssemblies(this Assembly assembly)
    {
      var assemblies = new List<Assembly>();
      GetDependentAssemblies(assembly, assemblies);
      assemblies.Add(assembly);
      return assemblies;
    }

    public static void AddDiForDependentAssemblies(this Assembly assembly, ContainerBuilder builder,
      IConfiguration configuration)
    {
      var assemblies = assembly.GetAssemblies();
      var injectedClasses = assemblies
        .SelectMany(a =>
          a.GetTypes().Where(type => type.BaseType == typeof(Module)).ToList()
        ).ToList();

      foreach (var type in injectedClasses)
      {
        var method = type.GetMethod("Load", BindingFlags.Instance | BindingFlags.NonPublic);
        if (method == null || !method.GetParameters().Any(i => i.ParameterType == typeof(ContainerBuilder)))
          return;

        if (Activator.CreateInstance(type, null) is IModule module)
          builder.RegisterModule(module);
      }
    }

    private static void GetDependentAssemblies(Assembly assembly, ICollection<Assembly> dependentAssemblyList)
    {
      var referencedAssemblies = assembly.GetReferencedAssemblies()
        .Where(i => i.FullName.StartsWith("LocalizationEditor"));

      foreach (var referencedAssembly in referencedAssemblies)
      {
        if (dependentAssemblyList.Any(i => i.FullName == referencedAssembly.FullName))
          continue;
        var loadedAssembly = Assembly.Load(referencedAssembly.FullName);
        dependentAssemblyList.Add(loadedAssembly);
        GetDependentAssemblies(loadedAssembly, dependentAssemblyList);
      }
    }
  }
}