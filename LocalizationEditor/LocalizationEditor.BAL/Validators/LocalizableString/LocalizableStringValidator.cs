using FluentValidation;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.Validators.LocalizableString
{
  internal class LocalizationRowValidator :  AbstractValidator<ILocalizationRow>
  {
    public LocalizationRowValidator(LocalizationPairValidator localizationPairValidator,
      LocalizationGroupValidator groupValidator)
    {
      RuleFor(row => row.LocalizationKey)
        .Must(localizationKey => !string.IsNullOrWhiteSpace(localizationKey));

      RuleFor(row => row.LocalizationGroup)
        .SetValidator(groupValidator);
      RuleFor(row => row.Localizations)
        .ForEach(localization => localization.SetValidator(localizationPairValidator));
    }
  }
}