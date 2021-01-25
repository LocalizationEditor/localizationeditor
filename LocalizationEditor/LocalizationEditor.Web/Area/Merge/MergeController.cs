using System.Threading.Tasks;
using LocalizationEditor.Merge.Service;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationEditor.Web.Area.Merge
{
  [ApiController]
  [Route("merge")]
  public class MergeController : ControllerBase
  {
    private readonly IMergeService _mergeService;

    public MergeController(IMergeService mergeService)
    {
      _mergeService = mergeService;
    }

    [HttpGet]
    public async Task Merge()
    {
      _mergeService.Merge(null, null);
    }
  }
}