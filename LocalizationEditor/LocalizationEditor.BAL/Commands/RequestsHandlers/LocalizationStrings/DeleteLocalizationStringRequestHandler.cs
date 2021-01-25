using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class DeleteLocalizationStringRequestHandler : IRequestHandler<DeleteLocalizationStringRequest, long>
  {
    private readonly ILocalizationStringRepository _repository;

    public DeleteLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<long> Handle(DeleteLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      string[] locales = new[] { "TextEn", "TextRu", "TextUa" };
      const string ConnectionString = @"Server=slukashov\sqlexpress;User=prockstest;Database=RocksTestV3;Password=F@mj8p2*~I0WZyRj;";
      _repository.SetConnectionString(ConnectionString);
      _repository.SetLocaleColumnNames(locales);
      var localizationRow = await _repository.GetByIdAsync(request.Id);
      return await _repository.DeleteAsync(localizationRow);
    }
  }
}