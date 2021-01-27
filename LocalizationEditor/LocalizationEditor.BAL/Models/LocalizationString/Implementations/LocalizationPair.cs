namespace LocalizationEditor.BAL.Models.LocalizationString.Implementations
{
  public class LocalizationPair : ILocalizationPair
  {
    public LocalizationPair(string locale, string value)
    {
      Locale = locale;
      Value = value;
    }

    public string Locale { get; set; }
    public string Value { get; set; }

    public LocalizationPair()
    {
    }
  }
}