using ConnectionString.Models;
using FluentValidation;

namespace ConnectionString.Validator
{
  public class ConnectionValidator : AbstractValidator<IConnection>
  {
    private const int MinLength = 1;
    private const int MaxLength = 256;

    public ConnectionValidator()
    {
      RuleFor(i => i.Id)
        .Must(id => id >= 0);

      RuleFor(i => i.Name)
        .NotNull()
        .NotEmpty()
        .Length(MinLength, MaxLength);

      RuleFor(i => i.ConnectionString)
        .NotNull()
        .NotEmpty()
        .Length(MinLength, MaxLength);
    }
  }
}