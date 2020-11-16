using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ConnectionString.Infrastructure.Services
{
  public interface IValidationService<TDomain>
  {
    void Validate(AbstractValidator<TDomain> validator, TDomain domain, ILogger logger);
  }
}