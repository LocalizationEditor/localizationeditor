using Microsoft.Extensions.Options;

namespace LocalizationEditor.Admin.Options
{
  internal class PathOptionsProvider : IPathOptionsProvider
  {
    public string Auth { get; }

    public PathOptionsProvider(IOptions<PathOptions> options)
    {
      Auth = options.Value.Auth;
    }
  }
}