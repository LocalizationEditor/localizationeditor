using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task Merge(IConnection sourceConnection, IConnection destinationConnection)
    {
      var sourceConnectionString = _connectionStringResolverService.GetConnectionResolver(sourceConnection).GetConnectionString();
      var destinationConnectionString = _connectionStringResolverService.GetConnectionResolver(destinationConnection).GetConnectionString();

      var localizationDto = _diffService.GetDiff(sourceConnectionString, destinationConnectionString);

      await Synchronize(localizationDto, destinationConnectionString);
    }

    private async Task Synchronize(ILocalizationDiffDto diffDto, string destinationConnection)
    {
      foreach (var addKey in diffDto.AddKeys)
        await _localizationStringRepository.SetConnectionString(destinationConnection).AddAsync(addKey);

      foreach (var removeKey in diffDto.RemoveKeys)
        await _localizationStringRepository.SetConnectionString(destinationConnection).DeleteAsync(removeKey);

      foreach (var editKey in diffDto.EditKeys)
        await _localizationStringRepository.SetConnectionString(destinationConnection).UpdateAsync(editKey);
    }
  }
}