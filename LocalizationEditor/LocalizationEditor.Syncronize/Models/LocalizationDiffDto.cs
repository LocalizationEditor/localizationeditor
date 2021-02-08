using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections;
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
}
