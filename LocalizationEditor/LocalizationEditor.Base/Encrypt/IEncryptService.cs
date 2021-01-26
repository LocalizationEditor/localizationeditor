namespace LocalizationEditor.Base.Encrypt
{
  public interface IEncryptService
  {
    string Encrypt(string src);
    string Decrypt(string src);
  }
}