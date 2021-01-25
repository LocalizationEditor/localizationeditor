using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class DeleteLocalizationStringRequestHandler : IRequestHandler<IDeleteLocalizationStringRequest, long>
  {
    private readonly ILocalizationStringRepository _repository;

    public DeleteLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<long> Handle(IDeleteLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      var localizationRow = await _repository.GetByIdAsync(request.Id);
      return await _repository.DeleteAsync(localizationRow, null);
    }
  }
}