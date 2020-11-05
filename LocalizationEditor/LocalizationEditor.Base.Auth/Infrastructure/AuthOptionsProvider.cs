using System.Text;
using LocalizationEditor.Base.Auth.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LocalizationEditor.Base.Auth.Infrastructure
{
  internal class AuthOptionsProvider : IAuthOptionsProvider
  {
    public string Issuer { get; }
    public string Audience { get; }
    public string Secret { get; }
    public int TokenLifeTime { get; }

    public AuthOptionsProvider(IOptions<AuthOptions> options)
    {
      Issuer = options.Value.Issuer;
      Audience = options.Value.Audience;
      Secret = options.Value.Secret;
      TokenLifeTime = options.Value.TokenLifeTime;
    }

    public SymmetricSecurityKey GetSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    }
  }
}