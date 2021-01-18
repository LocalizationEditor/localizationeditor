using FluentValidation;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.Validators.LocalizableString
{
  internal class LocalizationRowValidator :  AbstractValidator<ILocalizationString>
  {
    public LocalizationRowValidator(LocalizationPairValidator localizationPairValidator,
      LocalizationGroupValidator groupValidator)
    {
      RuleFor(row => row.Key)
        .Must(localizationKey => !string.IsNullOrWhiteSpace(localizationKey));

      RuleFor(row => row.Group)
        .SetValidator(groupValidator);
      RuleFor(row => row.Localizations)
        .ForEach(localization => localization.SetValidator(localizationPairValidator));
    }
  }
}