namespace LocalizationEditor.DAL.Models
{
  public class DbLocalizationGroup
  {
    public long Id { get; set; }
    public string Name { get; set; }
  }

  public class DbLocalizationKey
  {
    public long Id { get; set; }
    public string Key { get; set; }
  }

  public class DbLocalizationString
  {
    public long Id { get; set; }
    public string Locale { get; set; }
    public string Value { get; set; }
  }
}