using AutoMapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class LocalizationGroupViewMapperProfile : Profile
  {
    public LocalizationGroupViewMapperProfile()
    {
      CreateMap<ILocalizationGroup, LocalizationStringGroupView>()
        .ForMember(destinationMember => destinationMember.Name,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Name));
    }
  }
}