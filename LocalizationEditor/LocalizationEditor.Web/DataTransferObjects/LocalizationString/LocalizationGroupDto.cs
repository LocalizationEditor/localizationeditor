using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.Web.DataTransferObjects.LocalizationString
{
  internal class LocalizationGroupDto : ILocalizationGroup
  {
    public LocalizationGroupDto(long id, string name)
    {
      Id = id;
      Name = name;
    }

    public long Id { get; }
    public string Name { get; }
  }
}