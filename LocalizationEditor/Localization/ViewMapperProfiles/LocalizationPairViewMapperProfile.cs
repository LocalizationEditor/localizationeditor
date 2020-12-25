using AutoMapper;
using Localization.DataTransferObjects.LocalizationString;
using Localization.ViewModels.LocalizationStrings;

namespace Localization.ViewMapperProfiles
{
  public class LocalizationPairViewMapperProfile : Profile
  {
    public LocalizationPairViewMapperProfile()
    {
      CreateMap<LocalizationPairDto, LocalizationPairView>()
        .ForMember(destinationMember => destinationMember.Locale,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Locale))
        .ForMember(destinationMember => destinationMember.Value,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Value))
        .ReverseMap()
        .ConstructUsing(source => new LocalizationPairDto(source.Locale, source.Value));
    }
  }
}