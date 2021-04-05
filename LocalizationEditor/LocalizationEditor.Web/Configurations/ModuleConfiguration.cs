using Autofac;
using LocalizationEditor.Web.ViewMapperProfiles;

namespace LocalizationEditor.Web.Configuration
{
  public class ModuleConfiguration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<LocalizationItemViewMapper>()
        .As<ILocalizationItemViewMapper>()
        .InstancePerLifetimeScope();
    }
  }
}