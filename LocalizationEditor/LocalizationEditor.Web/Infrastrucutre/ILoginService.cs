using LocalizationEditor.Admin.Models;
using Microsoft.AspNetCore.Http;

namespace LocalizationEditor.Web.Infrastrucutre
{
  public interface ILoginService
  {
    void Login(HttpContext context, IUser user);
  }
}
