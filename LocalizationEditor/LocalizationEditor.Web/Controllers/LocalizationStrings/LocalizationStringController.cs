using AutoMapper;
using LocalizationEditor.BAL.Configurations;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Web.Attributes;
using LocalizationEditor.Web.Controllers.Core;
using LocalizationEditor.Web.ViewMapperProfiles;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Controllers.LocalizationStrings
{
  [Route("localization")]
  [LocalizationAuth]
  public class LocalizationStringController : LocalizationEditorController
  {
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITablesConfigurationOptions _tablesConfigurationOptions;
    private readonly ILocalizationItemViewMapper _localizationItemViewMapper;

    public LocalizationStringController(IMapper mapper,
      IMediator mediator,
      ITablesConfigurationOptions tablesConfigurationOptions,
      IConnectionService service,
      ILocalizationItemViewMapper localizationItemViewMapper) : base(service)
    {
      _mapper = mapper;
      _mediator = mediator;
      _tablesConfigurationOptions = tablesConfigurationOptions;
      _localizationItemViewMapper = localizationItemViewMapper;
    }

    [HttpPost]
    public async Task<ActionResult<LocalizationStringItemView>> Add(LocalizationStringItemView view)
    {
      var dto = await _localizationItemViewMapper.GetDomain(view, Connection);
      var request = new AddLocalizationStringRequest(dto, Connection);
      dto = await _mediator.Send(request);
      return _localizationItemViewMapper.GetView(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocalizationStringItemView>> Update(long id, LocalizationStringItemView view)
    {
      var dto = await _localizationItemViewMapper.GetDomain(view, Connection);
      var request = new UpdateLocalizationStringRequest(id, dto, Connection);
      dto = await _mediator.Send(request);
      return _localizationItemViewMapper.GetView(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      var request = new DeleteLocalizationStringRequest(id, Connection);
      await _mediator.Send(request);
      return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<LocalizationStringListView>> GetAll(int limit, int offset, string search)
    {
      var request = new GetAllLocalizationStringRequest
      {
        Search = search,
        Connection = Connection
      };
      var items = await _mediator.Send(request);

      return new LocalizationStringListView
      {
        LocalizationStrings = items
          .Skip(offset)
          .Take(limit)
          .Select(_localizationItemViewMapper.GetView).OrderBy(i => i.Group).ThenBy(i => i.Key),
        Count = items.Count()
      };
    }

    [HttpGet("editor")]
    public async Task<ActionResult<LocalizationStringItemView>> GetEditorModel(LocalizationEditorQueryView view)
    {
      var request = new SearchLocalizationStringRequest(view.GroupKey, view.LocalizationStringKey, Connection);
      var localizationString = await _mediator.Send(request);
      return _mapper.Map<LocalizationStringItemView>(localizationString);
    }

    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(_tablesConfigurationOptions.Locales);
    }

    [HttpGet("editor/config")]
    public async Task<ActionResult<LocalizationStringsEditorConfig>> GetEditorConfig()
    {
      var request = new GetAllLocalizationGroupsRequest(Connection);
      var results = await _mediator.Send(request);
      return new LocalizationStringsEditorConfig
      {
        Locales = _tablesConfigurationOptions.Locales,
        Groups = results.Select(i => i.Name)
      };
    }
  }
}