using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal class UpdateLocalizationStringRequestHandler : IRequestHandler<UpdateLocalizationStringRequest, ILocalizationString>
  {
    private readonly ILocalizationStringRepository _repository;

    public UpdateLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<ILocalizationString> Handle(UpdateLocalizationStringRequest request, CancellationToken cancellationToken)
    {
      const string ConnectionString = @"Server=AHAPTELMANOV\SQLEXPRESS;User=prockstest;Database=RocksTest;Password=F@mj8p2*~I0WZyRj;";
      _repository.SetConnectionString(ConnectionString);
      var modelFromDb = await _repository.GetByIdAsync(request.Id);
      modelFromDb.Update(request.LocalizationString);
      return await _repository.UpdateAsync(modelFromDb);
    }
  }
}