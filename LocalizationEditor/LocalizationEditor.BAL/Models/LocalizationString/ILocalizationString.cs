using LocalizationEditor.Base.Models;
using System;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationString : IIdentityModel, IComparable<ILocalizationString>
  {
    ILocalizationGroup Group { get; }
    string Key { get; }
    IReadOnlyCollection<ILocalizationPair> Localizations { get; }

    void Update(ILocalizationString localizationString);
    void UpdateGroup(ILocalizationGroup localizationGroup);
  }
}