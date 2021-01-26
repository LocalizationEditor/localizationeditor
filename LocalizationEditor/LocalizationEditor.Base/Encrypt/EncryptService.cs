using NETCore.Encrypt;

namespace LocalizationEditor.Base.Encrypt
{
  public class EncryptService : IEncryptService
  {
    private readonly IEncryptOptionProvider _optionProvider;

    public EncryptService(IEncryptOptionProvider optionProvider)
    {
      _optionProvider = optionProvider;
    }

    public string Encrypt(string src)
    {
      return EncryptProvider.AESEncrypt(src, _optionProvider.PublicKey);
    }

    public string Decrypt(string src)
    {
      return EncryptProvider.AESDecrypt(src, _optionProvider.PublicKey);
    }
  }
}