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
      const string ConnectionString = @"Server=AHAPTELMANOV\SQLEXPRESS;User=prockstest;Database=RocksTest;Password=F@mj8p2*~I0WZyRj;";
      _repository.SetConnectionString(ConnectionString);
      var localizationRow = await _repository.GetByIdAsync(request.Id);
      return await _repository.DeleteAsync(localizationRow);
    }
  }
}