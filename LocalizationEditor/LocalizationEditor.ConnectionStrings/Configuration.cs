using Autofac;
using LocalizationEditor.Base.Extensions;
using LocalizationEditor.ConnectionStrings.Options;
using LocalizationEditor.ConnectionStrings.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.ConnectionStrings
{
  public class Configuration : Module, IConfigurationModule
  {
    public IConfiguration ConfigurationRoot { get; set; }
    
    public void AddOptions(IServiceCollection services)
    {
      services.Configure<PathOptions>(ConfigurationRoot.GetSection(nameof(PathOptions)));
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<ConnectionService>().As<IConnectionService>();
      builder.RegisterType<PathOptionsProvider>().As<IPathOptionsProvider>();
      builder.RegisterType<ConnectionStringResolverService>().As<IConnectionStringResolverService>();
    }
  }
}