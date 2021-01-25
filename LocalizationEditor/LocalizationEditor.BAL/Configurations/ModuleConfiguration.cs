using Autofac;
using LocalizationEditor.BAL.Services.LocalizationStrings;
using LocalizationEditor.BAL.Services.LocalizationStrings.Implementations;

namespace LocalizationEditor.BAL.Configurations
{
  public class ModuleConfiguration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<LocalizationStringService>()
        .As<ILocalizationStringService>()
        .InstancePerLifetimeScope();


      builder
        .RegisterType<TableNamingOptions>()
        .As<ITableNamingOptions>()
        .InstancePerLifetimeScope();
    }
  }
}