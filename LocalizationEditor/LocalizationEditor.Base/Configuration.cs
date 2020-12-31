using Autofac;
using LocalizationEditor.Base.Encrypt;
using LocalizationEditor.Base.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.Base
{
public class Configuration : Module, IConfigurationModule
  {
    public IConfiguration ConfigurationRoot { get; private set; }
    public IHostingOption HostingOption { get; private set; }

    public void AddOptions(IServiceCollection services)
    {
      services.Configure<EncryptOption>(ConfigurationRoot.GetSection(nameof(EncryptOption)));
    }

    public void SetConfig(IConfiguration configuration, IHostingOption hostingOption)
    {
      ConfigurationRoot = configuration;
      HostingOption = hostingOption;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<EncryptService>().As<IEncryptService>();
      builder.RegisterType<EncryptOptionProvider>().As<IEncryptOptionProvider>();
    }
  }
}