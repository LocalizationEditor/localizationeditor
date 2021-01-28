using System.Threading;
using System.Threading.Tasks;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Services;
using MediatR;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  public class AddLocalizationStringRequestHandler : IRequestHandler<AddLocalizationStringRequest, ILocalizationString>
  {
    private readonly ILocalizationStringRepository _repository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public AddLocalizationStringRequestHandler(ILocalizationStringRepository repository,
      IConnectionStringResolverService connectionStringResolverService)
    {
      _repository = repository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<ILocalizationString> Handle(AddLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      const string ConnectionString = @"Server=slukashov\sqlexpress;User=prockstest;Database=RocksTestV3;Password=F@mj8p2*~I0WZyRj;";
      var connectionString = await _connectionStringResolverService.GetConnectionStringAsync(request.ConnectionStringKey);
      _repository.SetConnectionString(connectionString);
      return await _repository.AddAsync(request.LocalizationString);
    }
  }

}