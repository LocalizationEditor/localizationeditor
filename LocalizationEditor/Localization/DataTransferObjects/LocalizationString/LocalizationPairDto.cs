namespace Localization.DataTransferObjects.LocalizationString
{
  public class LocalizationPairDto
  {
    public LocalizationPairDto(string locale, string value)
    {
      Locale = locale;
      Value = value;
    }

    public string Locale { get; }
    public string Value { get; }
  }
}