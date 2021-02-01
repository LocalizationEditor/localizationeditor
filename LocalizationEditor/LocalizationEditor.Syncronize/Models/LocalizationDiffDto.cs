using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;

namespace LocalizationEditor.Syncronize.Models
{
  internal class LocalizationDiffDto : ILocalizationDiffDto
  {
    public IReadOnlyCollection<ILocalizationString> AddKeys { get; }
    public IReadOnlyCollection<ILocalizationString> RemoveKeys { get; }
    public IReadOnlyCollection<ILocalizationString> EditKeys { get; }

    public LocalizationDiffDto(
      IReadOnlyCollection<ILocalizationString> addKeys,
      IReadOnlyCollection<ILocalizationString> removeKeys,
      IReadOnlyCollection<ILocalizationString> editKeys)
    {
      AddKeys = addKeys;
      RemoveKeys = removeKeys;
      EditKeys = editKeys;
    }
  }

  public interface ILocalizationDiffDto
  {
    IReadOnlyCollection<ILocalizationString> AddKeys { get; }
    IReadOnlyCollection<ILocalizationString> RemoveKeys { get; }
    IReadOnlyCollection<ILocalizationString> EditKeys { get; }
  }

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
