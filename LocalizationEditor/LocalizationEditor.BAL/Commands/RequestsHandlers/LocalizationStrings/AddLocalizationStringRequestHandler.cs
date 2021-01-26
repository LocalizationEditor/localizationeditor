using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  public class AddLocalizationStringRequestHandler : IRequestHandler<AddLocalizationStringRequest, ILocalizationString>
  {
    private readonly ILocalizationStringRepository _repository;

    public AddLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public async Task<ILocalizationString> Handle(AddLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      const string ConnectionString = @"Server=slukashov\sqlexpress;User=prockstest;Database=RocksTestV3;Password=F@mj8p2*~I0WZyRj;";
      _repository.SetConnectionString(ConnectionString);
      return await _repository.AddAsync(request.LocalizationString);
    }
  }
}