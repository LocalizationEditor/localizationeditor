using System.Collections.Generic;

namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationRow : IIdentityModel
  {
    ILocalizationGroup LocalizationGroup { get; }
    string LocalizationKey { get; }
    IReadOnlyCollection<ILocalizationPair> Localizations { get; }
  }

  public interface ILocalizationGroup : IIdentityModel
  {
    string Name { get; }
  }

  public interface IIdentityModel
  {
    long Id { get; }
  }
}