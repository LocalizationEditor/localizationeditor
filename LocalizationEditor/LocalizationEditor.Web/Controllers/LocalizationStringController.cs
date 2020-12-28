using AutoMapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.Web.DataTransferObjects.LocalizationString;
using LocalizationEditor.Web.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.Controllers
{
  [ApiController, Route("localization")]
  public class LocalizationStringController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LocalizationStringController(IMapper mapper, IMediator mediator)
    {
      _mapper = mapper;
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LocalizationStringItemView>> Add(LocalizationStringItemView view)
    {
      var dto = _mapper.Map<ILocalizationRow>(view);
      var request = new AddLocalizationStringRequest(dto);
      dto = await _mediator.Send(request);
      return _mapper.Map<LocalizationStringItemView>(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocalizationStringItemView>> Update(long id, LocalizationStringItemView view)
    {
      var dto = _mapper.Map<ILocalizationRow>(view);
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
      IEnumerable<LocalizationRowDto> items = GetAllRows();
      // var request = new GetAllLocalizationStringRequest();
      //await _mediator.Send(request);

      return new LocalizationStringListView
      {
        LocalizationStrings = items
          .Select(_mapper.Map<LocalizationStringItemView>)
      };
    }

    private static IEnumerable<LocalizationRowDto> GetAllRows()
    {
      var item =
        new LocalizationRowDto(0,
          new LocalizationGroupDto(0, "group"),
          "key",
          new List<ILocalizationPair>
          {
            new LocalizationPairDto("ru", "sdfgsdfg"),
            new LocalizationPairDto("ua", "@222"),
            new LocalizationPairDto("en", "!11"),
          });

      var items = Enumerable.Repeat(item, 100);
      return items;
    }

    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(new[] {"ru", "ua", "en"});
    }
  }
}