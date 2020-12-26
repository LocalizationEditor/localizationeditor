using System.Linq;
using System.Reflection;
using Autofac;

namespace LocalizationEditor.Base.Extensions
{
  public static class AutofacExtensions
  {
    public static void AddDependencyInjection(this ContainerBuilder builder, Assembly rootAssembly)
    {
      var assemblies = rootAssembly.GetAssemblies().ToArray();
      builder.RegisterAssemblyModules(assemblies);
    }
  }
}