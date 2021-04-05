using LocalizationEditor.Base.Options;
using Microsoft.Extensions.Options;

namespace LocalizationEditor.Base.Infrastructure
{
  internal class CookiesOptionProvider : ICookiesOptionProvider
  {
    public string Key { get; }
    public int Expires { get; }

    public CookiesOptionProvider(IOptions<CookiesOption> options)
    {
      Key = options.Value.Key;
      Expires = options.Value.Expires;
    }
  }
}
