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
    public ILocalizationGroup Group { get; private set; }
    public string Key { get; private set; }
    public IReadOnlyCollection<ILocalizationPair> Localizations { get; private set; }

    public void Update(ILocalizationString localizationString)
    {
      Group = localizationString.Group;
      Key = localizationString.Key;
      Localizations = localizationString.Localizations;
    }
  }
}