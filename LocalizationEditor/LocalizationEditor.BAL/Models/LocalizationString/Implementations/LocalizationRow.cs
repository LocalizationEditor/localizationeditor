using System.Collections.Generic;

namespace LocalizationEditor.BAL.Models.LocalizationString.Implementations
{
  public class LocalizationString : ILocalizationString
  {
    public LocalizationString(long id,
      ILocalizationGroup localizationGroup,
      string localizationKey,
      List<ILocalizationPair> localizations)
    {
      Id = id;
      Group = localizationGroup;
      Key = localizationKey;
      Localizations = localizations;
    }

    public long Id { get; }
    public ILocalizationGroup Group { get; }
    public string Key { get; }
    public IReadOnlyCollection<ILocalizationPair> Localizations { get; }
  }
}