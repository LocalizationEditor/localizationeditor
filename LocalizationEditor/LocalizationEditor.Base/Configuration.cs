using Autofac;
using LocalizationEditor.Base.Encrypt;
using LocalizationEditor.Base.Extensions;
using LocalizationEditor.Base.Infrastructure;
using LocalizationEditor.Base.Options;
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
      services.Configure<CookiesOption>(ConfigurationRoot.GetSection(nameof(CookiesOption)));
      services.Configure<DomainsOption>(ConfigurationRoot.GetSection(nameof(DomainsOption)));
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
      builder.RegisterType<DomainsOptionProvider>().As<IDomainsOptionProvider>();
      builder.RegisterType<CookiesOptionProvider>().As<ICookiesOptionProvider>();
    }
  }
}