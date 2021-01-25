using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  public class AddLocalizationStringRequestHandler : IRequestHandler<AddLocalizationStringRequest, ILocalizationKey>
  {
    private readonly ILocalizationStringRepository _repository;

    public AddLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<ILocalizationKey> Handle(AddLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      return await _repository.AddAsync(request.LocalizationKey, null);
    }
  }
}