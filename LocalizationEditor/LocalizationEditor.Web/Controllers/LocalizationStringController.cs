using AutoMapper;
using LocalizationEditor.BAL.Configurations;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Services;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Controllers
{
  [ApiController, Route("localization")]
  public class LocalizationStringController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITablesConfigurationOptions _tablesConfigurationOptions;
    private readonly IConnectionService _service;

    public LocalizationStringController(IMapper mapper,
      IMediator mediator,
      ITablesConfigurationOptions tablesConfigurationOptions,
      IConnectionService service)
    {
      _mapper = mapper;
      _mediator = mediator;
      _tablesConfigurationOptions = tablesConfigurationOptions;
      _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<LocalizationStringItemView>> Add(LocalizationStringItemView view)
    {
      var connections = await _service.GetConnectionsAsync();
      
      var first = connections.First();
      var dto = _mapper.Map<ILocalizationString>(view);
      var request = new AddLocalizationStringRequest(dto) { ConnectionStringKey = first.ConnectionName };
      dto = await _mediator.Send(request);
      return _mapper.Map<LocalizationStringItemView>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocalizationStringItemView>> Update(long id, LocalizationStringItemView view)
    {
      var connections = await _service.GetConnectionsAsync();

      var first = connections.First();
      var dto = _mapper.Map<ILocalizationString>(view);
      var request = new UpdateLocalizationStringRequest(id, dto) { ConnectionStringKey = first.ConnectionName };
      dto = await _mediator.Send(request);
      view = _mapper.Map<LocalizationStringItemView>(dto);
      return view;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      var connections = await _service.GetConnectionsAsync();

      var first = connections.First();
      var request = new DeleteLocalizationStringRequest(id) { ConnectionStringKey = first.ConnectionName };
      await _mediator.Send(request);
      return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<LocalizationStringListView>> GetAll(int limit, int offset, string search)
    {
      var connections = await _service.GetConnectionsAsync();

      var first = connections.First();
      var request = new GetAllLocalizationStringRequest
      {
        Search = search  ,
        ConnectionStringKey = first.ConnectionName 
      };
      var items = await _mediator.Send(request);

      return new LocalizationStringListView
      {
        LocalizationStrings = items
          .Skip(offset)
          .Take(limit)
          .Select(_mapper.Map<LocalizationStringItemView>).OrderBy(i=>i.Group).ThenBy(i=>i.Key),
        Count = items.Count()
      };
    }

    [HttpGet("editor")]
    public async Task<ActionResult<LocalizationStringItemView>> GetEditorModel(LocalizationEditorQueryView view)
    {
      var connections = await _service.GetConnectionsAsync();

      var first = connections.First();
      var request = new SearchLocalizationStringRequest(view.GroupKey, view.LocalizationStringKey) { ConnectionStringKey = first.ConnectionName };
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
      var connections = await _service.GetConnectionsAsync();

      var first = connections.First();
      var request = new GetAllLocalizationGroupsRequest { ConnectionStringKey = first.ConnectionName};
      var results = await _mediator.Send(request);
      return new LocalizationStringsEditorConfig
      {
        Locales = _tablesConfigurationOptions.Locales,
        Groups = results.Select(i => i.Name)
      };
    }
  }

  public class LocalizationEditorQueryView
  {
    [JsonProperty("groupKey")]
    public string GroupKey { get; set; }
    [JsonProperty("localizationStringKey")]
    public string LocalizationStringKey { get; set; }
  }
}