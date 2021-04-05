using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Services;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class UpdateLocalizationStringRequestHandler : IRequestHandler<UpdateLocalizationStringRequest, ILocalizationString>
  {
    private readonly ILocalizationStringRepository _repository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public UpdateLocalizationStringRequestHandler(ILocalizationStringRepository repository,
      IConnectionStringResolverService connectionStringResolverService)
    {
      _repository = repository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<ILocalizationString> Handle(UpdateLocalizationStringRequest request, CancellationToken cancellationToken)
    {
      var connectionString = _connectionStringResolverService.GetConnectionString(request.Connection);
      _repository.SetConnectionString(connectionString);

      var modelFromDb = await _repository.GetByIdAsync(request.Id);
      modelFromDb.Update(request.LocalizationString);
      return await _repository.UpdateAsync(modelFromDb);
    }
  }
}