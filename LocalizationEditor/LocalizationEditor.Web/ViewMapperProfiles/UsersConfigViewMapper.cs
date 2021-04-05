using AutoMapper;
using LocalizationEditor.Admin.Models.Implementations;
using LocalizationEditor.Web.Controllers.ConnectionStrings;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class UsersConfigViewMapper : Profile
  {
    public UsersConfigViewMapper()
    {
      CreateMap<RoleType, IdNamePairView>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(item => (long)item))
        .ForMember(destinationMember => destinationMember.Name,
          memberOptions => memberOptions.MapFrom(item => item.ToString()));
    }
  }
}