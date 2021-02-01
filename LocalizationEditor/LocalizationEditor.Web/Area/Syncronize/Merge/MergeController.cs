using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Syncronize.Service;
using System.Threading.Tasks;
using LocalizationEditor.ConnectionStrings.Services;
using System;
using Newtonsoft.Json;

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

      await _mergeService.Merge(sourceConnection, destinationConnection);
      return NoContent();
    }
  }

  public class MergeConnectionIdView
  {
    [JsonProperty("sourceId")]
    public Guid SourceId { get; set; }
    [JsonProperty("destinationId")]
    public Guid DestinationId { get; set; }
  }
}
