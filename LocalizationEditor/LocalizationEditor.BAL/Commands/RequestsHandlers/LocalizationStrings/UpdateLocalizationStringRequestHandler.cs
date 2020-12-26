using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class UpdateLocalizationStringRequestHandler : IRequestHandler<IUpdateLocalizationStringRequest, ILocalizationRow>
  {
    private readonly ILocalizationStringRepository _repository;

    public UpdateLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<ILocalizationRow> Handle(IUpdateLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
     var modelFromDb =  await _repository.GetByIdAsync(request.Id);
     return await _repository.UpdateAsync(request.LocalizationString);
    }
  }
}