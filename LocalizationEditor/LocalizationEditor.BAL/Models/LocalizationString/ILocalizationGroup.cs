using LocalizationEditor.Base.Models;

namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationGroup : IIdentityModel
  {
    string Name { get; }
  }
}