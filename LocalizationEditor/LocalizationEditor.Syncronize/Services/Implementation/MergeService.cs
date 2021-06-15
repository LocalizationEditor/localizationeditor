using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.Admin.Models;
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
    private readonly IDiffService _diffService;
    private readonly ILocalizationGroupRepository _localizationGroupRepository;

    public MergeService(
      ILocalizationStringRepository localizationStringRepository,
      IConnectionStringResolverService connectionStringResolverService,
      IDiffService diffService,
      ILocalizationGroupRepository localizationGroupRepository)
    {
      _localizationStringRepository = localizationStringRepository;
      _connectionStringResolverService = connectionStringResolverService;
      _diffService = diffService;
      _localizationGroupRepository = localizationGroupRepository;
    }

    public async Task MergeAsync(IConnection source, IConnection destination, IUser user, IReadOnlyCollection<long> sourceIds = null)
    {
      var sourceConnectionString = await _connectionStringResolverService.GetConnectionStringAsync(source.ConnectionName, user);
      var destinationConnectionString = await _connectionStringResolverService.GetConnectionStringAsync(destination.ConnectionName, user);

      var localizationDto = await _diffService.GetDiffAsync(sourceConnectionString, destinationConnectionString);

      var items = await _localizationStringRepository.SetConnectionString(sourceConnectionString).GetByIdsAsync(sourceIds?.ToList());

      await SynchronizeAsync(localizationDto, destinationConnectionString, items);
    }

    private async Task SynchronizeAsync(ILocalizationDiffDto diffDto, string destinationConnection, IReadOnlyCollection<ILocalizationString> sources = null)
    {
      _localizationStringRepository.SetConnectionString(destinationConnection);
      _localizationGroupRepository.SetConnectionString(destinationConnection);

      await ProcessStringsAsync(diffDto.AddKeys, _localizationStringRepository.AddAsync, sources);

      foreach (var removeKey in GetLocalizationStrings(diffDto.RemoveKeys, sources))
        await _localizationStringRepository.DeleteAsync(removeKey);

      await ProcessStringsAsync(diffDto.EditKeys, _localizationStringRepository.UpdateAsync, sources);
    }

    private async Task ProcessStringsAsync(
      IReadOnlyCollection<ILocalizationString> localizationStrings,
      Func<ILocalizationString, Task<ILocalizationString>> functor,
      IReadOnlyCollection<ILocalizationString> sources = null)
    {
      foreach (var addKey in GetLocalizationStrings(localizationStrings, sources))
      {
        var localizationGroup = await _localizationGroupRepository.SearchByGroupKeyAsync(addKey.Group.Name);
        if (localizationGroup is null)
        {
          localizationGroup = new LocalizationGroup { Name = addKey.Group.Name };
          localizationGroup = await _localizationGroupRepository.AddAsync(localizationGroup);
          addKey.UpdateGroup(localizationGroup);
        }

        await functor(addKey);
      }
    }


    private IEnumerable<ILocalizationString> GetLocalizationStrings(
      IReadOnlyCollection<ILocalizationString> localizationStrings, IReadOnlyCollection<ILocalizationString> sources)
    {
      return sources == null || sources.Count == 0
        ? localizationStrings
        : localizationStrings.Where(i => sources.Any(j => i.Id == j.Id && i.Key == j.Key));
    }
  }
}