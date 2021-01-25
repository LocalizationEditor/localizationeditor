using AutoMapper;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using MediatR;
using System.Linq;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class LocalizationItemViewMapperProfile : Profile
  {
    public LocalizationItemViewMapperProfile(IMediator mediator)
    {
      CreateMap<ILocalizationString, LocalizationStringItemView>()
        .ForMember(destinationMember => destinationMember.Id,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Id))
        .ForMember(destinationMember => destinationMember.Group,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Group.Name))
      .ForMember(destinationMember => destinationMember.Key,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Key))
        .ForMember(destinationMember => destinationMember.Localizations,
          memberOptions => memberOptions.MapFrom(resolver => resolver.Localizations))
        .ReverseMap()
        .ConstructUsing((source, ctx) => new LocalizationString(source.Id,
          mediator.Send(new GetOrAddLocalizationGroupRequest(source.Group)).GetAwaiter().GetResult(), // todo: map from correct place
          source.Key,
          source.Localizations.Select(ctx.Mapper.Map<ILocalizationPair>).ToList()));
    }
  }
}