using Autofac;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.DAL.Repository.LocalizationString;

namespace LocalizationEditor.DAL.Configurations
{
  public class ModuleConfiguration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<LocalizationStringRepository>()
        .As<ILocalizationStringRepository>()
        .InstancePerLifetimeScope();

      builder
        .RegisterType<LocalizationGroupRepository>()
        .As<ILocalizationGroupRepository>()
        .InstancePerLifetimeScope();
    }
  }
}