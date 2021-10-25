using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.BAL.Models;
using LocalizationEditor.Syncronize.Models;
using System;
using LocalizationEditor.Admin.Models;
using System.Runtime.InteropServices;

namespace LocalizationEditor.Syncronize.Service
{
  internal class DiffService : IDiffService
  {
    private const string KeyPattern = "{0} {1}";
    private readonly ILocalizationStringRepository _localizationStringRepository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public DiffService(
      ILocalizationStringRepository localizationStringRepository,
      IConnectionStringResolverService connectionStringResolverService)
    {
      _localizationStringRepository = localizationStringRepository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task<ILocalizationDiffDto> GetDiffAsync(string sourceConnection, string destinationConnection)
    {
      var source = await _localizationStringRepository.SetConnectionString(sourceConnection).GetAllAsync();
      var destination = await _localizationStringRepository.SetConnectionString(destinationConnection).GetAllAsync();

      // exist in source and not exist in destination -> add
      // not exist in source and exist in destination => remove
      // exist in source and exist in destination => update

      var removeKeys = GetAddedOrRemovedKeys(destination, source);
      var addKeys = GetAddedOrRemovedKeys(source, destination);
      var editKeys = GetChangedKeys(source, destination);

      return new LocalizationDiffDto(addKeys, removeKeys, editKeys);
    }

    public async Task<LocalizationDiff> GetDiffAsync(IConnection source, IConnection destination, IUser user)
    {
      var sourceConnectionString = await _connectionStringResolverService.GetConnectionStringAsync(source.ConnectionName, user);
      var destinationConnectionString = await _connectionStringResolverService.GetConnectionStringAsync(destination.ConnectionName, user);

      var diff = await GetDiffAsync(sourceConnectionString, destinationConnectionString);

      var localizationStrings = new List<ILocalizationString>(diff.AddKeys).Concat(diff.EditKeys).Concat(diff.RemoveKeys);
      var localizationDescriptionDto = localizationStrings
        .Select(item => new LocalizationGroupKeyDto { Key = item.Key, Group = item.Group.Name })
        .ToArray();

      var sources = await _localizationStringRepository.SetConnectionString(sourceConnectionString).GetByKeysAsync(localizationDescriptionDto);
      var destinations = await _localizationStringRepository.SetConnectionString(destinationConnectionString).GetByKeysAsync(localizationDescriptionDto);

      return new LocalizationDiff(sources, destinations);
    }

    private static IReadOnlyCollection<ILocalizationString> GetAddedOrRemovedKeys(
      IEnumerable<ILocalizationString> first, IEnumerable<ILocalizationString> second)
    {
      return first
        .Where(i => second.All(j => i.CompareTo(j) != 0))
        .ToList();
    }

    private static IReadOnlyCollection<ILocalizationString> GetChangedKeys(
     IEnumerable<ILocalizationString> first, IEnumerable<ILocalizationString> second)
    {
      var sourceMap = GetKeyToLocalizationMap(first);
      var destinationMap = GetKeyToLocalizationMap(second);

      var result = new List<ILocalizationString>();

      foreach ((string key, ILocalizationString value) in sourceMap)
      {
        if (!destinationMap.ContainsKey(key))
          continue;

        var destinationValue = destinationMap[key];

        if (IsEditKey(value.Localizations, destinationValue.Localizations))
          result.Add(value);
      }

      return result;
    }

    private static Dictionary<string, ILocalizationString> GetKeyToLocalizationMap(
      IEnumerable<ILocalizationString> localizationStrings)
    {
      string GetKey(ILocalizationString localizationString) =>
        string.Format(KeyPattern, localizationString.Group.Name, localizationString.Key);

      return localizationStrings
        .GroupBy(GetKey)
        .ToDictionary(
          i => i.Key,
          i => i.First());
    }

    private static bool IsEditKey(IReadOnlyCollection<ILocalizationPair> first, IReadOnlyCollection<ILocalizationPair> second)
    {
      return first.Count != second.Count ||
             first.Any(i => second.Any(j => j.Locale == i.Locale && j.Value != i.Value));
    }
  }
}