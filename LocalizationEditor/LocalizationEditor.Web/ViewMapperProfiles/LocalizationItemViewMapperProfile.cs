using AutoMapper;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class LocalizationItemViewMapper : ILocalizationItemViewMapper
  {
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LocalizationItemViewMapper(IMediator mediator, IMapper mapper)
    {
      _mediator = mediator;
      _mapper = mapper;
    }

    public async Task<ILocalizationString> GetDomain(LocalizationStringItemView view, IConnection connection)
    {
      var group = await _mediator.Send(new GetOrAddLocalizationGroupRequest(view.Group, connection));
      return new LocalizationString(view.Id,
        group,
        view.Key,
        view.Localizations.Select(_mapper.Map<ILocalizationPair>).ToList());
    }

    public LocalizationStringItemView GetView(ILocalizationString model)
    {
      return new LocalizationStringItemView
      {
        Group = model.Group.Name,
        Id = model.Id,
        Key = model.Key,
        Localizations = model.Localizations.Select(_mapper.Map<LocalizationPairView>)
      };
    }
  }
}