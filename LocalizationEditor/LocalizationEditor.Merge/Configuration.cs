using Autofac;
using LocalizationEditor.Merge.Service;

namespace LocalizationEditor.Merge
{
  public class Configuration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<MergeService>().As<IMergeService>();
    }
  }
}