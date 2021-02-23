using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Syncronize.Service;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Services;

namespace LocalizationEditor.Web.Area.Syncronize.Merge
{
  [Route("syncronize/merge")]
  [ApiController]
  public partial class MergeController : ControllerBase
  {
    private readonly IMergeService _mergeService;
    private readonly IConnectionService _connectionService;

    public MergeController(
      IMergeService mergeService,
      IConnectionService connectionService)
    {
      _mergeService = mergeService;
      _connectionService = connectionService;
    }

    [HttpPost]
    public async Task<IActionResult> Merge([FromBody] SelectedMergeViewModel  mergeViewModel)
    {
      var sourceConnection = await _connectionService.GetConnectionByIdAsync(mergeViewModel.SourceId);
      var destinationConnection = await _connectionService.GetConnectionByIdAsync(mergeViewModel.DestinationId);

      await _mergeService.MergeAsync(sourceConnection, destinationConnection, mergeViewModel.SourceLocalizationIds);
      return NoContent();
    }
  }
}
