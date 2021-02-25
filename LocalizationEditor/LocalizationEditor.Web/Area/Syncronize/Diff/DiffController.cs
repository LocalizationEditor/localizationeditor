using AutoMapper;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Syncronize.Service;
using LocalizationEditor.Web.Area.Syncronize.Merge;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Area.Syncronize.Diff
{
  [Route("syncronize/diff")]
  [ApiController]
  public class DiffController : ControllerBase
  {
    private readonly IDiffService _diffService;
    private readonly IConnectionService _connectionService;
    private readonly IMapper _mapper;

    public DiffController(IDiffService diffService, IConnectionService connectionService, IMapper mapper)
    {
      _diffService = diffService;
      _connectionService = connectionService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<DiffViewModel> GetDiff([FromQuery]Guid sourceId, [FromQuery]Guid destinationId)
    {
      var sourceConnection = await _connectionService.GetConnectionByIdAsync(sourceId);
      var destinationConnection = await _connectionService.GetConnectionByIdAsync(destinationId);

      var dto = await _diffService.GetDiffAsync(sourceConnection, destinationConnection);
      return new DiffViewModel
      {
        Destinations = dto.Destination.Select(_mapper.Map<LocalizationStringItemView>).ToList(),
        Sources = dto.Sources.Select(_mapper.Map<LocalizationStringItemView>).ToList(),
      };
    }
  }
}
