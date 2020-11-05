namespace LocalizationEditor.Base.Utils
{
  internal class DbCoder : IDbCoder
  {
    private readonly Coder _coder;

    public DbCoder(Coder coder)
    {
      _coder = coder;
    }

    public string Encrypt(string data)
    {
      return _coder.Encrypt(data);
    }

    public string Decrypt(string encryptData)
    {
      return _coder.Decrypt(encryptData);
    }
  }
}