using LocalizationEditor.Base.Options;
using Microsoft.Extensions.Options;

namespace LocalizationEditor.Base.Infrastructure
{
  internal class DomainsOptionProvider : IDomainsOptionProvider
  {
    public string Domain { get; }

    public DomainsOptionProvider(IOptions<DomainsOption> options)
    {
      Domain = options.Value.Domain;
    }
  }
}
