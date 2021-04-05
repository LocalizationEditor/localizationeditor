using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;

namespace LocalizationEditor.Syncronize.Models
{
  public class LocalizationDiff
  {
    public IReadOnlyCollection<ILocalizationString> Sources { get; }
    public IReadOnlyCollection<ILocalizationString> Destination { get; }

    public LocalizationDiff(IReadOnlyCollection<ILocalizationString> sources, IReadOnlyCollection<ILocalizationString> destination)
    {
      Sources = sources;
      Destination = destination;
    }
  }
}
