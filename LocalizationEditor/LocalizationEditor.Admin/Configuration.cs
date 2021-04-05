using Autofac;
using LocalizationEditor.Base.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocalizationEditor.Admin.Options;
using LocalizationEditor.Admin.Services.Implementation;
using LocalizationEditor.Admin.Services;

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
      builder.RegisterType<PathOptionsProvider>().As<IPathOptionsProvider>();
      builder.RegisterType<UserService>().As<IUserService>();
    }
  }
}