using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Syncronize.Models;

namespace LocalizationEditor.Syncronize.Service
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

      var sourceTask = _localizationStringRepository.GetAllAsync();
      var destinationTask = _localizationStringRepository.GetAllAsync();

      Task.WaitAll(sourceTask, destinationTask);

      var source = sourceTask.Result;
      var destination = destinationTask.Result;

      // exist in source and not exist in destination -> add
      // not exist in source and exist in destination => remove
      // exist in source and exist in destination => update

      var removeKeys = GetAddOrRemoveKey(destination, source);
      var addKeys = GetAddOrRemoveKey(source, destination);
      var editKeys = GetEditKey(source, destination);

      var dto = new LocalizationDiffDto(addKeys, removeKeys, editKeys);
      await Synchronize(dto, null);
    }

    private async Task Synchronize(LocalizationDiffDto diffDto, IDbConnection destinationConnection)
    {
      foreach (var addKey in diffDto.AddKeys)
        await _localizationStringRepository.AddAsync(addKey);

      foreach (var removeKey in diffDto.RemoveKeys)
        await _localizationStringRepository.DeleteAsync(removeKey);

      foreach (var editKey in diffDto.EditKeys)
        await _localizationStringRepository.UpdateAsync(editKey);
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