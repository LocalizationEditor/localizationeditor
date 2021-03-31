using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Syncronize.Service;
using LocalizationEditor.Web.Controllers;
using LocalizationEditor.Web.ViewMapperProfiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Area.Syncronize.Diff
{
  [Route("syncronize/diff")]
  public class DiffController : LocalizationEditorController
  {
    private readonly IDiffService _diffService;
    private readonly ILocalizationItemViewMapper _localizationStringItemViewMapper;

    public DiffController(IDiffService diffService,
      IConnectionService connectionService,
      ILocalizationItemViewMapper localizationStringItemViewMapper) : base(connectionService)
    {
      _diffService = diffService;
      _localizationStringItemViewMapper = localizationStringItemViewMapper;
    }

    [HttpGet]
    public async Task<DiffViewModel> GetDiff([FromQuery] Guid destinationId)
    {
      var sourceConnection = Connection;
      var destinationConnection = await ConnectionService.GetConnectionByIdAsync(destinationId);

      var dto = await _diffService.GetDiffAsync(sourceConnection, destinationConnection);
      return new DiffViewModel
      {
        Destinations = dto.Destination.Select(_localizationStringItemViewMapper.GetView).ToList(),
        Sources = dto.Sources.Select(_localizationStringItemViewMapper.GetView).ToList(),
      };
    }
  }
}
