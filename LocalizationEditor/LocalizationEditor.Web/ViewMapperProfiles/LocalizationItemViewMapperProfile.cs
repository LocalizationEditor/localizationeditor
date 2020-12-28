using AutoMapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.Web.DataTransferObjects.LocalizationString;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using System.Linq;

namespace LocalizationEditor.Web.ViewMapperProfiles
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
          null, // todo: map from correct place
          source.Key,
          source.Localizations.Select(ctx.Mapper.Map<ILocalizationPair>).ToList()));
    }
  }

  internal class LocalizationGroupViewMapperProfile : Profile
  {
    public LocalizationGroupViewMapperProfile()
    {
      CreateMap<ILocalizationGroup, LocalizationStringGroupView>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Id))
        .ForMember(destinationMember => destinationMember.Name,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Name))
        ;
    }
  }
}