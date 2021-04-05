using Microsoft.Extensions.Options;

namespace LocalizationEditor.ConnectionStrings.Options
{
  internal class PathOptionsProvider : IPathOptionsProvider
  {
    public string FileName { get; }

    public PathOptionsProvider(IOptions<PathOptions> options)
    {
      FileName = options.Value.FileName;
    }
  }
}