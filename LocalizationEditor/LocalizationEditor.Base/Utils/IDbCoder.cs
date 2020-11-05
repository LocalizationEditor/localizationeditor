namespace LocalizationEditor.Base.Utils
{
  public interface IDbCoder
  {
    string Encrypt(string data);
    string Decrypt(string encryptData);
  }
}