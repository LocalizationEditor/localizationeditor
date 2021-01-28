using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Services;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class GetAllocalizationGroupsRequestHandler : IRequestHandler<GetAllLocalizationGroupsRequest, IEnumerable<ILocalizationGroup>>
  {
    private readonly ILocalizationGroupRepository _repository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public GetAllocalizationGroupsRequestHandler(ILocalizationGroupRepository repository, IConnectionStringResolverService connectionStringResolverService)
    {
      _repository = repository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<IEnumerable<ILocalizationGroup>> Handle(GetAllLocalizationGroupsRequest request, CancellationToken cancellationToken)
    {
      var connectionString = await _connectionStringResolverService.GetConnectionStringAsync(request.ConnectionStringKey);
      _repository.SetConnectionString(connectionString);
      return await _repository.GetAllAsync();
    }
  }
}