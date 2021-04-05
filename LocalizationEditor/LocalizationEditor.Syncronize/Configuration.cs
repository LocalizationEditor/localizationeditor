using Autofac;
using LocalizationEditor.Syncronize.Service;

namespace LocalizationEditor.Syncronize
{
  public class Configuration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<MergeService>().As<IMergeService>().InstancePerLifetimeScope();
      builder.RegisterType<DiffService>().As<IDiffService>().InstancePerLifetimeScope();
    }
  }
}
