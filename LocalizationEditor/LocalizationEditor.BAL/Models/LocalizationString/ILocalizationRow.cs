using System.Collections.Generic;

namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationRow
  {
    long Id { get; }
    string LocalizationGroup { get; }
    string LocalizationKey { get; }
    IReadOnlyCollection<ILocalizationPair> Localizations { get; }
  }
}