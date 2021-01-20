using Autofac;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.DAL.Repository;

namespace LocalizationEditor.DAL
{
  public class Configuration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<LocalizationGroupRepository>().As<ILocalizationGroupRepository>();
      builder.RegisterType<LocalizationStringRepository>().As<ILocalizationStringRepository>();
    }
  }

  public class Costil
  {

  }
}