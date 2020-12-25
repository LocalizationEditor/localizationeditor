using System.Collections.Generic;

namespace Localization.DataTransferObjects.LocalizationString
{
  public class LocalizationRowDto
  {
    public LocalizationRowDto(long id,
      string localizationGroup,
      string localizationKey,
      List<LocalizationPairDto> localizations)
    {
      Id = id;
      LocalizationGroup = localizationGroup;
      LocalizationKey = localizationKey;
      Localizations = localizations;
    }

    public long Id { get; }
    public string LocalizationGroup { get; }
    public string LocalizationKey { get; }
    public IReadOnlyCollection<LocalizationPairDto> Localizations { get; }
  }
}