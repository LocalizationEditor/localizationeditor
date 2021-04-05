using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Web.Controllers.Core;
using LocalizationEditor.Admin.Models.Implementations;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Services;
using LocalizationEditor.Web.Attributes;

namespace LocalizationEditor.Web.Controllers.ConnectionStrings
{
  [Route("users")]
  [LocalizationAuth]
  public class UserManagmentController : LocalizationEditorController
  {
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserManagmentController(IUserService service,
      IConnectionService connectionService,
      IMapper mapper)
      : base(connectionService)
    {
      _service = service;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserView>>> GetConnectionsAsync()
    {
      var connections = await _service.GetAll();
      return connections != null
        ? connections.Select(_mapper.Map<UserView>).ToList()
        : null;
    }

    [HttpGet("config")]
    public ActionResult<IReadOnlyCollection<IdNamePairView>> GetConnectionConfig()
    {
      return EnumExtensions.GetValueList<RoleType>()
        .Select(item => _mapper.Map<IdNamePairView>(item))
        .ToList();
    }

    [HttpPost]
    public async Task<ActionResult<UserView>> Create(UserView model)
    {
      var dto = _mapper.Map<IUser>(model);
      await _service.Add(dto);
      return _mapper.Map<UserView>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserView>> Update(Guid id, UserView model)
    {
      var dto = _mapper.Map<IUser>(model);
      await _service.Update(id, dto);
      return model;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
      await _service.Remove(id);
      return id;
    }

    [HttpGet("getUserById")]
    public async Task<ActionResult<UserView>> GetById(Guid id)
    {
      var user = await _service.GetById(id);
      return _mapper.Map<UserView>(user);
    }
  }
}