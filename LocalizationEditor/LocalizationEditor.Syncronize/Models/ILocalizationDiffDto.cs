using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;

namespace LocalizationEditor.Syncronize.Models
{
  public interface ILocalizationDiffDto
  {
    IReadOnlyCollection<ILocalizationString> AddKeys { get; }
    IReadOnlyCollection<ILocalizationString> RemoveKeys { get; }
    IReadOnlyCollection<ILocalizationString> EditKeys { get; }
  }
}
