using System;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ConnectionString.Infrastructure.Services
{
  internal class ValidationService<TDomain> : IValidationService<TDomain>
  {
    public void Validate(AbstractValidator<TDomain> validator, TDomain domain, ILogger logger)
    {
      try
      {
        validator.ValidateAndThrow(domain);
      }
      catch(Exception ex)
      {
        logger.LogInformation($"message {ex.Message}, stacktrace: {ex.StackTrace}");
        throw;
      }
    }
  }
}