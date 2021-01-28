using LocalizationEditor.Base.Models;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationString : IIdentityModel
  {
    ILocalizationGroup Group { get; }
    string Key { get; }
    IReadOnlyCollection<ILocalizationPair> Localizations { get; }

    void Update(ILocalizationString localizationString);
  }
}