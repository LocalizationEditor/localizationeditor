using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocalizationEditor.Web.Controllers
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

    private IConnection GetConnectionName()
    {
      var isParsed = Guid.TryParse(Request.Headers["X-Connection"], out var guid);
      if (isParsed)
        return ConnectionService.GetConnectionByIdAsync(guid).GetAwaiter().GetResult();
      throw new ConnectionNotFoundException();
    }
  }
}