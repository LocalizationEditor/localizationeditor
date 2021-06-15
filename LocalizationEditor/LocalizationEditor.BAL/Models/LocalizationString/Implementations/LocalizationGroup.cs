namespace LocalizationEditor.BAL.Models.LocalizationString.Implementations
{
  public class LocalizationGroup : ILocalizationGroup
  {
    public LocalizationGroup(long id, string name)
    {
      Id = id;
      Name = name;
    }

    public long Id { get; private set; }
    public string Name { get; }

    public void UdpateId(long id)
    {
      Id = id;
    }
  }
}