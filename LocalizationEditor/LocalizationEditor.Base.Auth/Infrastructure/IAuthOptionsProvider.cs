using LocalizationEditor.Base.Auth.Options;
using Microsoft.IdentityModel.Tokens;

namespace LocalizationEditor.Base.Auth.Infrastructure
{
  public interface IAuthOptionsProvider : IAuthOptions
  {
    SymmetricSecurityKey GetSecurityKey();
  }
}