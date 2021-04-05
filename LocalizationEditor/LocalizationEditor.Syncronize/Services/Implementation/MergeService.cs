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

    public MergeService(
      ILocalizationStringRepository localizationStringRepository,
      IConnectionStringResolverService connectionStringResolverService,
      IDiffService diffService)
    {
      _localizationStringRepository = localizationStringRepository;
      _connectionStringResolverService = connectionStringResolverService;
      _diffService = diffService;
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

      foreach (var addKey in GetLocalizationStrings(diffDto.AddKeys, sources))
        await _localizationStringRepository.AddAsync(addKey);

      foreach (var removeKey in GetLocalizationStrings(diffDto.RemoveKeys, sources))
        await _localizationStringRepository.DeleteAsync(removeKey);

      foreach (var editKey in GetLocalizationStrings(diffDto.EditKeys, sources))
        await _localizationStringRepository.UpdateAsync(editKey);
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