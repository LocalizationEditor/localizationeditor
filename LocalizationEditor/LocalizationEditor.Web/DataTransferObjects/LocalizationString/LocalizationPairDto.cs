using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.Web.DataTransferObjects.LocalizationString
{
  internal class LocalizationPairDto : ILocalizationPair
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