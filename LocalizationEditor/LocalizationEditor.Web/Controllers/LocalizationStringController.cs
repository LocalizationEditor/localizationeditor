using AutoMapper;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
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

    public LocalizationStringController(IMapper mapper,
      IMediator mediator)
    {
      _mapper = mapper;
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LocalizationStringItemView>> Add(LocalizationStringItemView view)
    {
      var dto = _mapper.Map<ILocalizationString>(view);
      var request = new AddLocalizationStringRequest(dto);
      dto = await _mediator.Send(request);
      return _mapper.Map<LocalizationStringItemView>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocalizationStringItemView>> Update(long id, LocalizationStringItemView view)
    {
      var dto = _mapper.Map<ILocalizationString>(view);
      var request = new UpdateLocalizationStringRequest(id, dto);
      dto = await _mediator.Send(request);
      view = _mapper.Map<LocalizationStringItemView>(dto);
      return view;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      var request = new DeleteLocalizationStringRequest(id);
      await _mediator.Send(request);
      return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<LocalizationStringListView>> GetAll(int limit, int offset, string search)
    {
      var request = new GetAllLocalizationStringRequest
      {
        Search = search
      };
      var items = await _mediator.Send(request);

      return new LocalizationStringListView
      {
        LocalizationStrings = items
          .Skip(offset)
          .Take(limit)
          .Select(_mapper.Map<LocalizationStringItemView>),
        Count = items.Count()
      };
    }

    [HttpGet("editor")]
    public async Task<ActionResult<LocalizationStringItemView>> GetEditorModel(LocalizationEditorQueryView view)
    {
      var request = new SearchLocalizationStringRequest(view.GroupKey, view.LocalizationStringKey);
      var localizationString = await _mediator.Send(request);
      return _mapper.Map<LocalizationStringItemView>(localizationString);
    }


    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(new[] { "TextEn", "TextRu", "TextUa" });
    }

    [HttpGet("editor/config")]
    public ActionResult<LocalizationStringsEditorConfig> GetEditroConfig()
    {
      return new LocalizationStringsEditorConfig
      {
        Locales = new[] { "TextEn", "TextRu", "TextUa" },
        Groups = new[] { "Core.CommonStrings" }
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