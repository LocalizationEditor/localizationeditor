using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationEditor.Base.Extensions
{
  public interface IConfigurationModule
  {
    IConfiguration ConfigurationRoot { get; set; }

    void AddOptions(IServiceCollection services);
  }
}