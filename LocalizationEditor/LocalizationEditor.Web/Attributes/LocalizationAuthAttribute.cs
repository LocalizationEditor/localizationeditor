using System;
using LocalizationEditor.Admin.Services;
using LocalizationEditor.Base.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LocalizationEditor.Web.Attributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class LocalizationAuthAttribute : Attribute, IAuthorizationFilter
  {
    private readonly bool _allowAnonymus;

    public LocalizationAuthAttribute(bool allowAnonymus)
    {
      _allowAnonymus = allowAnonymus;
    }

    public LocalizationAuthAttribute()
    {
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      if (_allowAnonymus)
        return;

      var options = context.HttpContext.RequestServices.GetService(typeof(ICookiesOptionProvider)) as ICookiesOptionProvider;
      var userService = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
      var cookie = context.HttpContext.Request.Cookies[options.Key];
      if (string.IsNullOrEmpty(cookie) || userService.GetById(Guid.Parse(cookie)).Result == null)
      {
        context.Result = new UnauthorizedResult();
      }
    }
  }
}
