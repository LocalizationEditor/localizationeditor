using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Syncronize.Service;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Services;

namespace LocalizationEditor.Web.Area.Syncronize.Merge
{
  [Route("syncronize/merge")]
  [ApiController]
  public class MergeController : ControllerBase
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
    public async Task<IActionResult> Merge(MergeConnectionIdView connectionsId)
    {
      var sourceConnection = await _connectionService.GetConnectionByIdAsync(connectionsId.SourceId);
      var destinationConnection = await _connectionService.GetConnectionByIdAsync(connectionsId.DestinationId);

      await _mergeService.MergeAsync(sourceConnection, destinationConnection);
      return NoContent();
    }

    [HttpPost("selected")]
    public async Task<IActionResult> Merge(SelectedMergeViewModel selectedMergeModel)
    {
      var sourceConnection = await _connectionService.GetConnectionByIdAsync(selectedMergeModel.SourceId);
      var destinationConnection = await _connectionService.GetConnectionByIdAsync(selectedMergeModel.DestinationId);

      await _mergeService.MergeAsync(sourceConnection, destinationConnection, selectedMergeModel.SourceLocalizationIds);
      return NoContent();
    }
  }
}
