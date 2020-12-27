using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class AddLocalizationStringRequestHandler : IRequestHandler<IAddLocalizationStringRequest, ILocalizationRow>
  {
    private readonly ILocalizationStringRepository _repository;

    public AddLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<ILocalizationRow> Handle(IAddLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      return await _repository.AddAsync(request.LocalizationString);
    }
  }
}