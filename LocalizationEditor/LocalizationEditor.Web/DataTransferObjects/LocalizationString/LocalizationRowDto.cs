using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;

namespace LocalizationEditor.Web.DataTransferObjects.LocalizationString
{
  internal class LocalizationRowDto : ILocalizationRow
  {
    public LocalizationRowDto(long id,
      ILocalizationGroup localizationGroup,
      string localizationKey,
      List<ILocalizationPair> localizations)
    {
      Id = id;
      LocalizationGroup = localizationGroup;
      LocalizationKey = localizationKey;
      Localizations = localizations;
    }

    public long Id { get; }
    public ILocalizationGroup LocalizationGroup { get; }
    public string LocalizationKey { get; }
    public IReadOnlyCollection<ILocalizationPair> Localizations { get; }
  }
}