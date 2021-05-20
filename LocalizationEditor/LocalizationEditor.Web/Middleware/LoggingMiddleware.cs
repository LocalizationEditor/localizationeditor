using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Middleware
{
  public class LoggingMiddleware
  {
    private readonly RequestDelegate _requestDelegate;
    private readonly ILogger _logger;

    public LoggingMiddleware(RequestDelegate requestDelegate, ILoggerFactory factory)
    {
      _requestDelegate = requestDelegate;
      _logger = factory.CreateLogger<LoggingMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _requestDelegate.Invoke(context);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Message: {ex.Message} || StackTrace: {ex.StackTrace}");
        throw;
      }
    }
  }
}
