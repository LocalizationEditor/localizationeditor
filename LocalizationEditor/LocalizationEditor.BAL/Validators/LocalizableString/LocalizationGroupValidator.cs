using FluentValidation;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.Validators.LocalizableString
{
  internal class LocalizationGroupValidator :  AbstractValidator<ILocalizationGroup>
  {
    public LocalizationGroupValidator()
    {
      RuleFor(row => row.Name)
        .Must(groupName => !string.IsNullOrWhiteSpace(groupName));
    }
  }
}