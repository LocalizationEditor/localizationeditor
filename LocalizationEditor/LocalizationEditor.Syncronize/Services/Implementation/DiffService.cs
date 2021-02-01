using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.BAL.Models;
using LocalizationEditor.Syncronize.Models;

namespace LocalizationEditor.Syncronize.Service
{
  internal class DiffService : IDiffService
  {
    private readonly ILocalizationStringRepository _localizationStringRepository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public DiffService(
      ILocalizationStringRepository localizationStringRepository,
      IConnectionStringResolverService connectionStringResolverService)
    {
      _localizationStringRepository = localizationStringRepository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public ILocalizationDiffDto GetDiff(string sourceConnection, string destinationConnection)
    {
      var sourceTask = _localizationStringRepository.SetConnectionString(sourceConnection).GetAllAsync();
      var destinationTask = _localizationStringRepository.SetConnectionString(destinationConnection).GetAllAsync();

      Task.WaitAll(sourceTask, destinationTask);

      var source = sourceTask.Result;
      var destination = destinationTask.Result;

      // exist in source and not exist in destination -> add
      // not exist in source and exist in destination => remove
      // exist in source and exist in destination => update

      var removeKeys = GetAddOrRemoveKey(destination, source);
      var addKeys = GetAddOrRemoveKey(source, destination);
      var editKeys = GetEditKey(source, destination);

      return new LocalizationDiffDto(addKeys, removeKeys, editKeys);
    }

    public async Task<LocalizationDiff> GetDiffAsync(IConnection source, IConnection destination)
    {
      var sourceConnectionString = _connectionStringResolverService.GetConnectionResolver(source).GetConnectionString();
      var destinationConnectionString = _connectionStringResolverService.GetConnectionResolver(destination).GetConnectionString();
      var diff = GetDiff(sourceConnectionString, destinationConnectionString);

      var localizationStrings = Enumerable.Empty<ILocalizationString>().Concat(diff.AddKeys).Concat(diff.EditKeys).Concat(diff.RemoveKeys).ToList();
      var localizationDesriptionDto = localizationStrings.Select(item => new LocalizationGroupKeyDto { Key = item.Key, Group = item.Group.Name }).ToArray();

      var sources = await _localizationStringRepository.SetConnectionString(sourceConnectionString).GetByKeysAsync(localizationDesriptionDto);
      var destinations = await _localizationStringRepository.SetConnectionString(destinationConnectionString).GetByKeysAsync(localizationDesriptionDto);

      return new LocalizationDiff(sources, destinations);
    }

    private IReadOnlyCollection<ILocalizationString> GetAddOrRemoveKey(
      IEnumerable<ILocalizationString> first, IEnumerable<ILocalizationString> second)
    {
      var result = new List<ILocalizationString>();
      foreach (var x in first)
      {
        var isFound = false;
        foreach (var y in second)
        {
          if (x.CompareTo(y) == 0)
          {
            isFound = true;
            break;
          }
        }

        if (!isFound)
          result.Add(x);
      }

      return result;
    }

    private IReadOnlyCollection<ILocalizationString> GetEditKey(
      IEnumerable<ILocalizationString> first, IEnumerable<ILocalizationString> second)
    {
      var result = new List<ILocalizationString>();
      foreach (var x in first)
      {
        foreach (var y in second)
        {
          if (x.CompareTo(y) == 0 && IsEditKey(x, y))
          {
            result.Add(x);
            break;
          }
        }
      }

      return result;
    }

    private bool IsEditKey(ILocalizationString first, ILocalizationString second)
    {
      if (first.Localizations.Count != second.Localizations.Count)
        return true;

      return first.Localizations
        .Any(i =>
          second.Localizations
            .Any(j => j.Locale == i.Locale && j.Value != i.Value));
    }
  }
}