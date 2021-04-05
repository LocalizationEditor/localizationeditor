using LocalizationEditor.Admin.Models;
using LocalizationEditor.Base.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace LocalizationEditor.Web.Infrastrucutre
{
  internal class LoginService : ILoginService
  {
    private readonly IDomainsOptionProvider _domainsOptionProvider;
    private readonly ICookiesOptionProvider _cookiesOptionProvider;

    public LoginService(IDomainsOptionProvider domainsOptionProvider, ICookiesOptionProvider cookiesOptionProvider)
    {
      _domainsOptionProvider = domainsOptionProvider;
      _cookiesOptionProvider = cookiesOptionProvider;
    }

    public void Login(HttpContext context, IUser user)
    {
      context.User = new GenericPrincipal(new ClaimsIdentity(user.Email), new[] { user.Role.ToString() });
      context.Response.Cookies.Append(
        _cookiesOptionProvider.Key,
        user.Id.ToString(),
        new CookieOptions
        {
          Domain = _domainsOptionProvider.Domain,
          MaxAge = TimeSpan.FromMinutes(_cookiesOptionProvider.Expires),
          HttpOnly = true
        });
    }
  }
}
