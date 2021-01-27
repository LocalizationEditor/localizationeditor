using AutoMapper;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class LocalizationStringsConfigViewMapperProfile : Profile
  {
    public LocalizationStringsConfigViewMapperProfile()
    {
      CreateMap<IEnumerable<string>, LocalizationStringsConfigView>()
        .ForMember(destinationMember => destinationMember.Locales,
          memberOption => memberOption.MapFrom(i => i));
    }
  }
}