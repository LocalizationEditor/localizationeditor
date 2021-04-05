using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.Models.LocalizationString.Implementations
{
  public class LocalizationGroup : ILocalizationGroup
  {
    public LocalizationGroup(long id, string name)
    {
      Id = id;
      Name = name;
    }

    public long Id { get; }
    public string Name { get; }
  }
}