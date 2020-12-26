using AutoMapper;
using Localization.DataTransferObjects.LocalizationString;
using Localization.ViewModels.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Linq;

namespace Localization.ViewMapperProfiles
{
  internal class LocalizationItemViewMapperProfile : Profile
  {
    public LocalizationItemViewMapperProfile()
    {
      CreateMap<ILocalizationRow, LocalizationStringItemView>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Id))
        .ForMember(destinationMember => destinationMember.Group,
          memberOptions => memberOptions.MapFrom(resolver => resolver.LocalizationGroup))
        .ForMember(destinationMember => destinationMember.Key,
          memberOptions => memberOptions.MapFrom(resolver => resolver.LocalizationKey))
        .ForMember(destinationMember => destinationMember.Localizations,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Localizations))
        .ReverseMap()
        .ConstructUsing((source, ctx) => new LocalizationRowDto(source.Id,
          source.Group,
          source.Key,
          source.Localizations.Select(ctx.Mapper.Map<ILocalizationPair>).ToList()));
    }
  }
}