using AutoMapper;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Web.Controllers.Core;
using LocalizationEditor.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Controllers.Users
{
  [Route("login")]
  public class LoginController : LocalizationEditorController
  {
    private readonly IMapper _mapper;

    public LoginController(IConnectionService service, IMapper mapper)
      : base(service)
    {
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
    {
      var login = _mapper.Map<LoginViewModel, ILoginDto>(loginViewModel);


      return NoContent();
    }
  }
}
