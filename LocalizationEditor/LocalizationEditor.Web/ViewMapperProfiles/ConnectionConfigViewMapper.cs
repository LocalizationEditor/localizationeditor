using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Web.ViewModels.ConnectionStrings;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class ConnectionConfigViewMapper : Profile
  {
    public ConnectionConfigViewMapper()
    {
      CreateMap<DbType, ConnectionDbTypeViewModel>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(item => (long)item))
        .ForMember(destinationMember => destinationMember.Name,
          memberOptions => memberOptions.MapFrom(item => item.ToString()));
    }
  }
}