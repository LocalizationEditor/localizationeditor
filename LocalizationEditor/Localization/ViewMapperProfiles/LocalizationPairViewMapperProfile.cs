using AutoMapper;
using Localization.DataTransferObjects.LocalizationString;
using Localization.ViewModels.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace Localization.ViewMapperProfiles
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