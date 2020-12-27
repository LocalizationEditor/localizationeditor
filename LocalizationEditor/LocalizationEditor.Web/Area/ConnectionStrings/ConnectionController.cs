﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.ConnectionStrings.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationEditor.Web.Area.ConnectionStrings
{
  [ApiController]
  [Route("connection")]
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
      return connections != null
        ? connections.Select(_mapper.Map<ConnectionViewModel>).ToList()
        : null;
    }

    [HttpGet("config")]
    public IReadOnlyCollection<ConnectionConfigViewModel> GetConnectionConfig()
    {
      return Enum.GetValues(typeof(DbType)).Cast<DbType>()
        .Select(item => _mapper.Map<ConnectionConfigViewModel>(item))
        .ToList();
    }

    [HttpPost]
    public async Task<ConnectionViewModel> CreateConnectionString(ConnectionViewModel model)
    {
      var dto = _mapper.Map<IConnection>(model);
      await _service.SaveConnectionAsync(dto);
      return model;
    }

    [HttpPut]
    public async Task<ConnectionViewModel> Update(ConnectionViewModel model)
    {
      var dto = _mapper.Map<IConnection>(model);
      await _service.SaveConnectionAsync(dto);
      return model;
    }

    [HttpDelete("{id}")]
    public async Task<long> Delete(long id)
    {
      await _service.Remove(id);
      return id;
    }
  }
}