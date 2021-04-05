using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Web.ViewModels.ConnectionStrings;
using LocalizationEditor.Web.Controllers.Core;
using LocalizationEditor.Web.Attributes;

namespace LocalizationEditor.Web.Controllers.ConnectionStrings
{
  [Route("connection")]
  [LocalizationAuth]
  public class ConnectionController : LocalizationEditorController
  {
    private readonly IMapper _mapper;
    private readonly IConnectionManager _connectionManager;

    public ConnectionController(IConnectionService service, IMapper mapper, IConnectionManager connectionManager) : base(service)
    {
      _mapper = mapper;
      _connectionManager = connectionManager;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ConnectionViewModel>>> GetConnectionsAsync()
    {
      var connections = await _connectionManager.GetConnectionsAsync(CurrentUser);
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
      await _connectionManager.SaveConnectionAsync(dto, CurrentUser).ConfigureAwait(false);
      return _mapper.Map<ConnectionViewModel>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ConnectionViewModel>> Update(Guid id, ConnectionViewModel model)
    {
      var dto = _mapper.Map<IConnection>(model);
      await _connectionManager.UpdateConnection(id, dto, CurrentUser).ConfigureAwait(false);
      return model;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
      await _connectionManager.Remove(id, CurrentUser).ConfigureAwait(false);
      return id;
    }
  }
}