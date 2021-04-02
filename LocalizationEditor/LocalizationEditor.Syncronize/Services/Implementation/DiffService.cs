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
      var localizationDesriptionDto = localizationStrings
        .Select(item => new LocalizationGroupKeyDto { Key = item.Key, Group = item.Group.Name })
        .ToArray();

      var sources = await _localizationStringRepository.SetConnectionString(sourceConnectionString).GetByKeysAsync(localizationDesriptionDto);
      var destinations = await _localizationStringRepository.SetConnectionString(destinationConnectionString).GetByKeysAsync(localizationDesriptionDto);

      return new LocalizationDiff(sources, destinations);
    }

    private IReadOnlyCollection<ILocalizationString> GetAddedOrRemovedKeys(
      IEnumerable<ILocalizationString> first, IEnumerable<ILocalizationString> second)
    {
      return first
        .Where(i => !second.Any(j => i.CompareTo(j) == 0))
        .ToList();
    }

    private IReadOnlyCollection<ILocalizationString> GetChangedKeys(
     IEnumerable<ILocalizationString> first, IEnumerable<ILocalizationString> second)
    {
      return first
        .Where(i => second.Any(j => i.CompareTo(j) == 0 && IsEditKey(i, j)))
        .ToList();
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