using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Services;
using LocalizationEditor.Base.Infrastructure;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Web.Attribute;
using LocalizationEditor.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LocalizationEditor.Web.Controllers.Core
{
  [ApiController]
  public class LocalizationEditorController : ControllerBase
  {
    protected readonly IConnectionService ConnectionService;

    public LocalizationEditorController(IConnectionService service)
    {
      ConnectionService = service;
    }

    public IConnection Connection => GetConnectionName();

    public IUser CurrentUser => GetUser();

    private IConnection GetConnectionName()
    {
      var isParsed = Guid.TryParse(Request.Headers["x-connection"], out var guid);
      if (isParsed)
        return ConnectionService.GetConnectionByIdAsync(guid, CurrentUser).GetAwaiter().GetResult();
      throw new ConnectionNotFoundException();
    }

    private IUser GetUser()
    {
      var userService = HttpContext.RequestServices.GetRequiredService<IUserService>();
      var cookiesOptions = HttpContext.RequestServices.GetRequiredService<ICookiesOptionProvider>();

      var isParsed = Guid.TryParse(Request.Cookies[cookiesOptions.Key], out var userId);
      if (isParsed)
      {
        return userService.GetById(userId).Result;
      }

      HttpContext.Response.StatusCode = 401;
      throw new InvalidCastException("Cant cast userId");
    }
  }
}