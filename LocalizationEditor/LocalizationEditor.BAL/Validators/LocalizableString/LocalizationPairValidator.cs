using FluentValidation;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.Validators.LocalizableString
{
  internal abstract class LocalizationPairValidator :  AbstractValidator<ILocalizationPair>
  {
    public LocalizationPairValidator()
    {
      RuleFor(row => row.Locale)
        .Must(locale => !string.IsNullOrWhiteSpace(locale));
    }

  }
}