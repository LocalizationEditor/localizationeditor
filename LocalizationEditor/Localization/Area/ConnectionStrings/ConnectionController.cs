using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Area.ConnectionStrings
{
  [ApiController]
  [Route("connections")]
  public class ConnectionController : ControllerBase
  {
    private readonly IConnectionService _service;
    private readonly IMapper _mapper;

    public ConnectionController(IConnectionService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<ConnectionViewModel>> GetConnectionsAsync()
    {
      var connections = await _service.GetConnectionsAsync();
      return connections.Select(_mapper.Map<ConnectionViewModel>).ToList();
    }

    [HttpPost]
    public async Task<ConnectionViewModel> CreateConnectionString(ConnectionViewModel model)
    {
      var dto = _mapper.Map<IConnection>(model);
      await _service.SaveConnectionAsync(new List<IConnection> {dto});
      return model;
    }
  }
}