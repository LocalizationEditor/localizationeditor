using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;

namespace Localization.DataTransferObjects.LocalizationString
{
  internal class LocalizationRowDto : ILocalizationRow
  {
    public LocalizationRowDto(long id,
      string localizationGroup,
      string localizationKey,
      List<ILocalizationPair> localizations)
    {
      Id = id;
      LocalizationGroup = localizationGroup;
      LocalizationKey = localizationKey;
      Localizations = localizations;
    }

    public long Id { get; }
    public string LocalizationGroup { get; }
    public string LocalizationKey { get; }
    public IReadOnlyCollection<ILocalizationPair> Localizations { get; }
  }
}