using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Web.Controllers;

namespace LocalizationEditor.Web.Area.ConnectionStrings
{
  [Route("connection")]
  public class ConnectionController : LocalizationEditorController
  {
    private readonly IMapper _mapper;

    public ConnectionController(IConnectionService service, IMapper mapper) : base(service)
    {
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ConnectionViewModel>>> GetConnectionsAsync()
    {
      var connections = await ConnectionService.GetConnectionsAsync();
      return connections != null
        ? connections.Select(_mapper.Map<ConnectionViewModel>).ToList()
        : null;
    }

    [HttpGet("config")]
    public ActionResult<IReadOnlyCollection<ConnectionDbTypeViewModel>> GetConnectionConfig()
    {
      return EnumExtensions.GetValueList<DbType>()
        .Select(item => _mapper.Map<ConnectionDbTypeViewModel>(item))
        .ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ConnectionViewModel>> CreateConnectionString(ConnectionViewModel model)
    {
      var dto = _mapper.Map<IConnection>(model);
      await ConnectionService.SaveConnectionAsync(dto);
      return _mapper.Map<ConnectionViewModel>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ConnectionViewModel>> Update(Guid id, ConnectionViewModel model)
    {
      var dto = _mapper.Map<IConnection>(model);
      await ConnectionService.UpdateConnection(id, dto);
      return model;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
      await ConnectionService.Remove(id);
      return id;
    }
  }
}