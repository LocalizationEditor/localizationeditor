using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class GetAllocalizationGroupsRequestHandler : IRequestHandler<GetAllLocalizationGroupsRequest, IEnumerable<ILocalizationGroup>>
  {
    private readonly ILocalizationGroupRepository _repository;

    public GetAllocalizationGroupsRequestHandler(ILocalizationGroupRepository repository)
    {
      _repository = repository;
    }

    public async Task<IEnumerable<ILocalizationGroup>> Handle(GetAllLocalizationGroupsRequest request, CancellationToken cancellationToken)
    {
      return await _repository.GetAllAsync();
    }
  }
}