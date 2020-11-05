using System.Threading.Tasks;
using Auth.Application.Fetchers.Commands;
using Auth.Application.Fetchers.Queries;
using Localization.Area.Auth.Models;
using LocalizationEditor.Base.Auth.Services;
using LocalizationEditor.Base.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Area.Auth.Conntrollers
{
  [Route("auth")]
  [ApiController]
  [AllowAnonymous]
  public class AuthControllers : ControllerBase
  {
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public AuthControllers(IMediator mediator, IAuthService authService)
    {
      _mediator = mediator;
      _authService = authService;
    }

    [HttpPost]
    public async Task<JwtTokenView> Authenticate(LoginModelView modelView)
    {
      var model = await _mediator.Send(
        new GetByEmailAndPasswordQuery {Email = modelView.Email, Password = modelView.Password});

      return new JwtTokenView {Token = _authService.GenerateJwt(new IdNameModel(model.Id, model.Email))};
    }

    [HttpPost("registration")]
    public async Task<JwtTokenView> Registration([FromBody] LoginModelView modelView)
    {
      var model = await _mediator.Send(
        new AddUserCommand {Email = modelView.Email, Password = modelView.Password});

      return new JwtTokenView {Token = _authService.GenerateJwt(new IdNameModel(model.Id, model.Email))};
    }
  }
}