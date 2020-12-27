using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class GetAllLocalizationStringRequestHandler : IRequestHandler<IGetAllLocalizationStringRequest, IEnumerable<ILocalizationRow>>
  {
    private readonly ILocalizationStringRepository _repository;

    public GetAllLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<IEnumerable<ILocalizationRow>> Handle(IGetAllLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      return await _repository.GetAllAsync();
    }
  }
}