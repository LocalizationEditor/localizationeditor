using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class DeleteLocalizationStringRequestHandler : IRequestHandler<DeleteLocalizationStringRequest, long>
  {
    private readonly ILocalizationStringRepository _repository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public DeleteLocalizationStringRequestHandler(ILocalizationStringRepository repository, IConnectionStringResolverService connectionStringResolverService)
    {
      _repository = repository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<long> Handle(DeleteLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      var connectionString = _connectionStringResolverService.GetConnectionString(request.Connection);
      _repository.SetConnectionString(connectionString);
      var localizationRow = await _repository.GetByIdAsync(request.Id);
      return await _repository.DeleteAsync(localizationRow);
    }
  }
}