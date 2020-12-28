using AutoMapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.Web.DataTransferObjects.LocalizationString;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class LocalizationPairViewMapperProfile : Profile
  {
    public LocalizationPairViewMapperProfile()
    {
      CreateMap<ILocalizationPair, LocalizationPairView>()
        .ForMember(destinationMember => destinationMember.Locale,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Locale))
        .ForMember(destinationMember => destinationMember.Value,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Value))
        .ReverseMap()
        .ConstructUsing(source => new LocalizationPairDto(source.Locale, source.Value));
    }
  }
}