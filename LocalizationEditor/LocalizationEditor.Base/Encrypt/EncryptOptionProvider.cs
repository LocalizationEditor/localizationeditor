using Microsoft.Extensions.Options;

namespace LocalizationEditor.Base.Encrypt
{
  internal class EncryptOptionProvider : IEncryptOptionProvider
  {
    public string PublicKey { get; }

    public EncryptOptionProvider(IOptions<EncryptOption> options)
    {
      PublicKey = options.Value.PublicKey;
    }
  }
}