using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
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

    public GetAllLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;

    }

    public async Task<IEnumerable<ILocalizationString>> Handle(GetAllLocalizationStringRequest request,
      CancellationToken cancellationToken)
    {
      string[] locales = new[] { "TextEn", "TextRu", "TextUa" };
      const string ConnectionString = @"Server=slukashov\sqlexpress;User=prockstest;Database=RocksTestV3;Password=F@mj8p2*~I0WZyRj;";
      _repository.SetConnectionString(ConnectionString);
      _repository.SetLocaleColumnNames(locales);

      var all = await _repository.GetAllAsync();

      return all.Where(i => string.IsNullOrEmpty(request.Search) ||
      !string.IsNullOrEmpty(request.Search) && (i.Group.Name.Search(request.Search) ||
                                                i.Key.Search(request.Search) ||
                                                i.Localizations.Any(j => j.Value?.Search(request.Search) == true)));
    }
  }

  class SearchLocalizationStringRequestHandler : IRequestHandler<SearchLocalizationStringRequest, ILocalizationString>
  {
    private readonly ILocalizationStringRepository _repository;

    public SearchLocalizationStringRequestHandler(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public Task<ILocalizationString> Handle(SearchLocalizationStringRequest request, CancellationToken cancellationToken)
    {

      return null;
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