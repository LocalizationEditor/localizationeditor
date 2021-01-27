using System;
using LocalizationEditor.Base.Models;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationKey : IIdentityModel, IComparable<ILocalizationKey>
  {
    ILocalizationGroup Group { get; }
    string Key { get; }
    IList<ILocalizationPair> Localizations { get; }
    void AddLocalization(ILocalizationPair pair);
  }
}