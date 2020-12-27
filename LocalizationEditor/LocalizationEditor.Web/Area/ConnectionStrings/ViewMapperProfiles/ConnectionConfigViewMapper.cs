using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.Web.Area.ConnectionStrings.ViewMapperProfiles
{
  internal class ConnectionConfigViewMapper : Profile
  {
    public ConnectionConfigViewMapper()
    {
      CreateMap<DbType, ConnectionConfigViewModel>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(item => (long)item))
        .ForMember(destinationMember => destinationMember.Name,
          memberOptions => memberOptions.MapFrom(item => item.ToString()));
    }
  }
}