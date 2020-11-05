using Microsoft.IdentityModel.Tokens;

namespace LocalizationEditor.Base.Auth.Options
{
  public interface IAuthOptions
  {
    string Issuer { get; }
    string Audience { get; }
    string Secret { get; }
    int TokenLifeTime { get; }
  }
}