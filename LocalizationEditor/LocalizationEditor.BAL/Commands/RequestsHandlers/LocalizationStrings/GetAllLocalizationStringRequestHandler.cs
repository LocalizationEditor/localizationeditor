using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  public class GetAllLocalizationStringRequestHandler : IRequestHandler<GetAllLocalizationStringRequest, IEnumerable<ILocalizationString>>
  {
    private readonly ILocalizationStringRepository _repository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public GetAllLocalizationStringRequestHandler(ILocalizationStringRepository repository, IConnectionStringResolverService connectionStringResolverService)
    {
      _repository = repository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<IEnumerable<ILocalizationString>> Handle(GetAllLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {

      var connectionString = await _connectionStringResolverService.GetConnectionStringAsync(request.ConnectionStringKey);
      _repository.SetConnectionString(connectionString);

      var all = await _repository.GetAllAsync();

      return all.Where(i => string.IsNullOrEmpty(request.Search) ||
      !string.IsNullOrEmpty(request.Search) && (i.Group.Name.Search(request.Search) ||
                                                i.Key.Search(request.Search) ||
                                                i.Localizations.Any(j => j.Value?.Search(request.Search) == true)));
    }
  }

  public static class StringExtension
  {
    public static bool Search(this string s, string search)
    {
      if (string.IsNullOrWhiteSpace(search))
        return true;

      return s.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
    }
  }
}