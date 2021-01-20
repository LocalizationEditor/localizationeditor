using AutoMapper;
using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
// using LocalizationEditor.Web.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
      var dto = _mapper.Map<ILocalizationKey>(view);
      var request = new AddLocalizationStringRequest(dto);
      dto = await _mediator.Send(request);
      return _mapper.Map<LocalizationStringItemView>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocalizationStringItemView>> Update(long id, LocalizationStringItemView view)
    {
      var dto = _mapper.Map<ILocalizationKey>(view);
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
    public async Task<ActionResult<LocalizationStringListView>> GetAll()
    {
      var request = new GetAllLocalizationStringRequest();
      var items = await _mediator.Send(request);

      var view = new LocalizationStringListView
      {
        LocalizationStrings = items
          .Select(_mapper.Map<LocalizationStringItemView>)
      };

      return view;
    }


    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(new[] {"ru", "ua", "en"});
    }
  }
}