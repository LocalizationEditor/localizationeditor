using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;

namespace LocalizationEditor.Merge.Service
{
  public interface IMergeService
  {
    Task Merge(IConnection source, IConnection destination);
  }

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

      var source = await _localizationStringRepository.GetAllAsync(sourceConnectionResolver.GetConnection());
      var destination = await _localizationStringRepository.GetAllAsync(destinationConnectionResolver.GetConnection());


    }
  }

  internal class LocalizationDiffDto
  {
    public IReadOnlyCollection<ILocalizationKey> AddKeys { get; }
    public IReadOnlyCollection<ILocalizationKey> RemoveKeys { get; }
    public IReadOnlyCollection<ILocalizationKey> EditKeys { get; }

    public LocalizationDiffDto()
    {
      AddKeys = new List<ILocalizationKey>();
      RemoveKeys = new List<ILocalizationKey>();
      EditKeys = new List<ILocalizationKey>();
    }
  }
}