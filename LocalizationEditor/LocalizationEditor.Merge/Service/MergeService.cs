using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Merge.Models;

namespace LocalizationEditor.Merge.Service
{
  internal class MergeService : IMergeService
  {
    private readonly ILocalizationStringRepository _localizationStringRepository;
    private readonly IConnectionStringResolverService _connectionStringResolverService;

    public MergeService(
      ILocalizationStringRepository localizationStringRepository,
      IConnectionStringResolverService connectionStringResolverService)
    {
      _localizationStringRepository = localizationStringRepository;
      _connectionStringResolverService = connectionStringResolverService;
    }

    public async Task Merge(IConnection sourceConnection, IConnection destinationConnection)
    {
      var sourceConnectionResolver = _connectionStringResolverService.GetConnectionResolver(sourceConnection);
      var destinationConnectionResolver = _connectionStringResolverService.GetConnectionResolver(destinationConnection);

      var sourceL = _localizationStringRepository.GetAllAsync(sourceConnectionResolver.GetConnection());
      var destinationL = _localizationStringRepository.GetAllAsync(destinationConnectionResolver.GetConnection());

      Task.WaitAll(sourceL, destinationL);

      var source = new List<ILocalizationKey>
      {
        new LocalizationKey(0, new LocalizationGroup(0, "group_1"), "key_1",
          new List<ILocalizationPair>
          {
            new LocalizationPair("en", "key_1_sourceLocal1"),
            new LocalizationPair("ru", "key_1_sourceLocal2"),
            new LocalizationPair("ua", "key_1_sourceLocal3"),
          }),
      };

      var destination = new List<ILocalizationKey>
      {
        new LocalizationKey(0, new LocalizationGroup(0, "group_2"), "key_2",
          new List<ILocalizationPair>
          {
            new LocalizationPair("en", "key_1_sourceLocal1"),
            new LocalizationPair("ru", "key_1_sourceLocal2"),
            new LocalizationPair("ua", "key_1_sourceLocal3"),
          }),
        new LocalizationKey(0, new LocalizationGroup(0, "group_3"), "key_3",
          new List<ILocalizationPair>
          {
            new LocalizationPair("en", "key_3_sourceLocal1"),
            new LocalizationPair("ru", "key_3_sourceLocal2"),
            new LocalizationPair("ua", "key_3_sourceLocal3"),
          }),
        new LocalizationKey(0, new LocalizationGroup(0, "group_1"), "key_1",
          new List<ILocalizationPair>
          {
            new LocalizationPair("en", "key_1_sourceLocal2"),
            new LocalizationPair("ru", "key_1_sourceLocal1"),
            new LocalizationPair("ua", "key_1_sourceLocal3"),
          }),
      };

      // exist in source and not exist in destination -> add
      // not exist in source and exist in destination => remove
      // exist in source and exist in destination => update

      var removeKeys = GetAddOrRemoveKey(destination, source);
      var addKeys = GetAddOrRemoveKey(source, destination);
      var editKeys = GetEditKey(source, destination);

      var dto = new LocalizationDiffDto(addKeys, removeKeys, editKeys);
      await Synchronize(dto, destinationConnectionResolver.GetConnection());
    }

    private async Task Synchronize(LocalizationDiffDto diffDto, IDbConnection destinationConnection)
    {
      foreach (var addKey in diffDto.AddKeys)
        await _localizationStringRepository.AddAsync(addKey, destinationConnection);

      foreach (var removeKey in diffDto.RemoveKeys)
        await _localizationStringRepository.DeleteAsync(removeKey, destinationConnection);

      foreach (var editKey in diffDto.EditKeys)
        await _localizationStringRepository.UpdateAsync(editKey, destinationConnection);
    }

    private IReadOnlyCollection<ILocalizationKey> GetAddOrRemoveKey(
      IEnumerable<ILocalizationKey> first, IEnumerable<ILocalizationKey> second)
    {
      var result = new List<ILocalizationKey>();
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

    private IReadOnlyCollection<ILocalizationKey> GetEditKey(
      IEnumerable<ILocalizationKey> first, IEnumerable<ILocalizationKey> second)
    {
      var result = new List<ILocalizationKey>();
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

    private bool IsEditKey(ILocalizationKey first, ILocalizationKey second)
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