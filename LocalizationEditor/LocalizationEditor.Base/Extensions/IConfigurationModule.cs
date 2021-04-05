using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.Base.Extensions
{
  public interface IConfigurationModule
  {
    IConfiguration ConfigurationRoot { get; }
    IHostingOption HostingOption { get; }
    void AddOptions(IServiceCollection services);

    void SetConfig(IConfiguration configuration, IHostingOption hostingOption);
  }
}