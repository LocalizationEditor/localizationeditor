using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Syncronize.Service;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Web.ViewModels.Merge;
using LocalizationEditor.Web.Controllers.Core;

namespace LocalizationEditor.Web.Controllers.Merge
{
  [Route("syncronize/merge")]
  [ApiController]
  public partial class MergeController : LocalizationEditorController
  {
    private readonly IMergeService _mergeService;

    public MergeController(
      IMergeService mergeService,
      IConnectionService connectionService) : base(connectionService)
    {
      _mergeService = mergeService;
    }

    [HttpPost]
    public async Task<IActionResult> Merge([FromBody] SelectedMergeViewModel mergeViewModel)
    {
      var sourceConnection = Connection;
      var destinationConnection = await ConnectionService.GetConnectionByIdAsync(mergeViewModel.DestinationId);

      await _mergeService.MergeAsync(sourceConnection, destinationConnection, mergeViewModel.SourceLocalizationIds);
      return NoContent();
    }
  }
}
