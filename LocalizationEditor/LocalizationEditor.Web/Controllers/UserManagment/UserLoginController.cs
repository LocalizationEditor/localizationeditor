using System.Threading.Tasks;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Services;
using Microsoft.AspNetCore.Mvc;
using LocalizationEditor.Web.Controllers.Core;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Services;
using LocalizationEditor.Web.ViewModels.Users;
using LocalizationEditor.Web.Infrastrucutre;
using LocalizationEditor.Web.Attributes;

namespace LocalizationEditor.Web.Controllers.ConnectionStrings
{
  [Route("users/login")]
  [LocalizationAuth(true)]
  public class UserLoginController : LocalizationEditorController
  {
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly ILoginService _loginService;

    public UserLoginController(IUserService service,
     IConnectionService connectionService,
     IMapper mapper,
     ILoginService loginService)
     : base(connectionService)
    {
      _service = service;
      _mapper = mapper;
      _loginService = loginService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Login([FromBody] LoginViewModel loginViewModel)
    {
      var dto = _mapper.Map<LoginViewModel, ILoginDto>(loginViewModel);
      var user = await _service.Login(dto);
      if (user == null)
        return new ForbidResult();

      _loginService.Login(HttpContext, user);
      return (int)user.Role;
    }
  }
}