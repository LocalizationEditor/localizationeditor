using AutoMapper;
using Localization.DataTransferObjects.LocalizationString;
using Localization.MediatR.Requests.LocalizationStrings;
using Localization.ViewModels.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Localization.Controllers
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
      var request = new GetAllLocalizationStringRequest();
      var item =
        new LocalizationRowDto(0,
         "group",
         "key",
         new List<ILocalizationPair>
         {
           new LocalizationPairDto("ru", "sdfgsdfg"),
           new LocalizationPairDto("ua", "@222"),
           new LocalizationPairDto("en", "!11"),
         });

       var items = Enumerable.Repeat(item, 100);
      //await _mediator.Send(request);

      return new LocalizationStringListView
      {
        LocalizationStrings = items
          .Select(_mapper.Map<LocalizationStringItemView>)
      };
    }

    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(new[] {"ru", "ua", "en"});
    }
  }
}