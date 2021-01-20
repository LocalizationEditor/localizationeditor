using System.Collections.Generic;
using System.Linq;

namespace LocalizationEditor.BAL.Models.LocalizationString.Implementations
{
  public class LocalizationKey : ILocalizationKey
  {
    public LocalizationKey(long id,
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
    public IList<ILocalizationPair> Localizations { get; }
    public void AddLocalization(ILocalizationPair pair)
    {
      Localizations.Add(pair);
    }
  }
}