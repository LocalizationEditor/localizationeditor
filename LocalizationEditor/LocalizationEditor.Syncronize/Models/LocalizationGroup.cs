using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.Syncronize.Models
{
  public class LocalizationGroup : ILocalizationGroup
  {
    public string Name { get; init; }
    public long Id { get; init; }
  }
}