using AutoMapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using System.Linq;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class LocalizationItemViewMapperProfile : Profile
  {
    public LocalizationItemViewMapperProfile()
    {
      CreateMap<ILocalizationKey, LocalizationStringItemView>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Id))
        .ForMember(destinationMember => destinationMember.Group,
          member => member.MapFrom(res => res.Group))
        .ForMember(destinationMember => destinationMember.Key,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Key))
        .ForMember(destinationMember => destinationMember.Localizations,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Localizations))
        .ReverseMap()
        .ConstructUsing((source, ctx) => new LocalizationKey(source.Id,
          ctx.Mapper.Map<LocalizationStringGroupView, ILocalizationGroup>(source.Group),
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
        .ReverseMap()
        .ConstructUsing(source => new LocalizationGroup(source.Id, source.Name));
    }
  }
}