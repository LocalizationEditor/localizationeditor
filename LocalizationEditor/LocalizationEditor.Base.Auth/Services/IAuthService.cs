using LocalizationEditor.Base.Models;

namespace LocalizationEditor.Base.Auth.Services
{
  public interface IAuthService
  {
    string GenerateJwt(IdNameModel entity);
  }
}