using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class GetOrAddLocalizationGroupRequestHandler : IRequestHandler<GetOrAddLocalizationGroupRequest, ILocalizationGroup>
  {
    private readonly ILocalizationGroupRepository _repository;

    public GetOrAddLocalizationGroupRequestHandler(ILocalizationGroupRepository repository)
    {
      _repository = repository;
    }

    public async Task<ILocalizationGroup> Handle(GetOrAddLocalizationGroupRequest request, CancellationToken cancellationToken)
    {
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