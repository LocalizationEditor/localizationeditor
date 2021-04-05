namespace LocalizationEditor.BAL.Models.LocalizationString
{
  public interface ILocalizationPair
  {
    string Locale { get; }
    string Value { get; }
  }
}