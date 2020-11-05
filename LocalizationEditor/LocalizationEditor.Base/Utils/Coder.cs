using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LocalizationEditor.Base.Utils
{
  public class Coder
  {
    private const string Key = "9NZXRnJVNLsrVRsiHv13Kxc7j6jmOI8=";
    private const string IV = "3pi7DwNXCj5te9==";

    public string Encrypt(string text)
    {
      using var aes = Aes.Create();
      return EncryptAes(text);
    }

    public string Decrypt(string encryptText)
    {
      using var aes = Aes.Create();
      return DecryptAes(Encoding.UTF8.GetBytes(encryptText));
    }

    private string EncryptAes(string plainText)
    {
      if (plainText == null || plainText.Length <= 0)
        throw new ArgumentNullException("plainText");

      byte[] encrypted;
      using var aesAlg = Aes.Create();
      aesAlg.Key = Encoding.UTF8.GetBytes(Key);
      aesAlg.IV = Encoding.UTF8.GetBytes(IV);

      var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

      using (var msEncrypt = new MemoryStream())
      {
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
          using (var swEncrypt = new StreamWriter(csEncrypt))
            swEncrypt.Write(plainText);

          encrypted = msEncrypt.ToArray();
        }
      }

      return Convert.ToBase64String(encrypted);
    }

    private string DecryptAes(byte[] cipherText)
    {
      // Check arguments.
      if (cipherText == null || cipherText.Length <= 0)
        throw new ArgumentNullException("cipherText");

      string plaintext = null;

      using (var aesAlg = Aes.Create())
      {
        aesAlg.Key = Encoding.UTF8.GetBytes(Key);
        aesAlg.IV = Encoding.UTF8.GetBytes(IV);

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using (var msDecrypt = new MemoryStream(cipherText))
        {
          using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
          {
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
              plaintext = srDecrypt.ReadToEnd();
            }
          }
        }
      }

      return plaintext;
    }
  }
}