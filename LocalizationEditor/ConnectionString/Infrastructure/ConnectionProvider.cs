using System.Threading.Tasks;
using ConnectionString.Infrastructure.Services;
using ConnectionString.Models;
using ConnectionString.Validator;
using Microsoft.Extensions.Logging;

namespace ConnectionString.Infrastructure
{
  internal class ConnectionProvider : IConnectionProvider
  {
    private readonly IValidationService<IConnection> _validationService;
    private readonly ConnectionValidator _validator;
    private readonly ILogger<ConnectionProvider> _logger;
    
    public ConnectionProvider(
      IValidationService<IConnection> validationService,
      ConnectionValidator validator,
      ILogger<ConnectionProvider> logger)
    {
      _validationService = validationService;
      _validator = validator;
      _logger = logger;
    }
    
    public async Task<IConnection> GetById(long id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IConnection> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public async Task<IConnection> Add(IConnection entity)
    {
      _validationService.Validate(_validator, entity, _logger);
      throw new System.NotImplementedException();
    }

    public async Task<IConnection> Update(IConnection entity)
    {
      _validationService.Validate(_validator, entity, _logger);
      throw new System.NotImplementedException();
    }

    public async Task Delete(IConnection entity)
    {
      throw new System.NotImplementedException();
    }
  }
}