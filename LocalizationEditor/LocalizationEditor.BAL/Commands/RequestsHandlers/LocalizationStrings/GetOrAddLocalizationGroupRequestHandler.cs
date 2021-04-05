using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Services;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class GetOrAddLocalizationGroupRequestHandler : IRequestHandler<GetOrAddLocalizationGroupRequest, ILocalizationGroup>
  {
    private readonly ILocalizationGroupRepository _repository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public GetOrAddLocalizationGroupRequestHandler(ILocalizationGroupRepository repository, IConnectionStringResolverService connectionStringResolverService)
    {
      _repository = repository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<ILocalizationGroup> Handle(GetOrAddLocalizationGroupRequest request, CancellationToken cancellationToken)
    {
      var connectionString = _connectionStringResolverService.GetConnectionString(request.Connection);
      _repository.SetConnectionString(connectionString);

      var group = await _repository.SearchByGroupKeyAsync(request.GroupName);
      if (group is null)
      {
        group = new LocalizationGroup(0, request.GroupName);
        group = await _repository.AddAsync(group);
      }
      return group;
    }
  }
}