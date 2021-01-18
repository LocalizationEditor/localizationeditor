using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  public class
    GetAllLocalizationStringRequestHandler : IRequestHandler<GetAllLocalizationStringRequest,
      IEnumerable<ILocalizationString>>
  {
    private readonly ILocalizationStringRepository _repository;

    public GetAllLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<IEnumerable<ILocalizationString>> Handle(GetAllLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      return await _repository.GetAllAsync();
    }
  }
}