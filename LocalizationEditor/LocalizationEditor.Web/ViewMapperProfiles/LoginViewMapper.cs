using AutoMapper;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;
using LocalizationEditor.Web.ViewModels.Users;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  public class LoginViewMapper : Profile
  {
    public LoginViewMapper()
    {
      CreateMap<LoginViewModel, ILoginDto>()
        .ConstructUsing(i => new LoginDto(i.Email, i.Password));
    }
  }
}
