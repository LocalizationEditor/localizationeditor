using Autofac;
using LocalizationEditor.Base.Extensions;
using LocalizationEditor.ConnectionStrings.Options;
using LocalizationEditor.ConnectionStrings.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings
{
  public class Configuration : Module, IConfigurationModule
  {
    public IConfiguration ConfigurationRoot { get; private set; }
    public IHostingOption HostingOption { get; private set; }

    public void AddOptions(IServiceCollection services)
    {
      services.Configure<PathOptions>(ConfigurationRoot.GetSection(nameof(PathOptions)));
    }

    public void SetConfig(IConfiguration configuration, IHostingOption hostingOption)
    {
      ConfigurationRoot = configuration;
      HostingOption = hostingOption;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<ConnectionService>().As<IConnectionService>();
      builder.RegisterType<PathOptionsProvider>().As<IPathOptionsProvider>();
      builder.RegisterType<ConnectionStringResolverService>().As<IConnectionStringResolverService>();
      builder.RegisterType<MssqlConnectionResolver>().As<IDataBaseConnectionResolver>();
      builder.RegisterType<MySqlConnectionResolver>().As<IDataBaseConnectionResolver>();
    }
  }
}