using AutoMapper;
using Localization.DataTransferObjects.LocalizationString;
using Localization.ViewModels.LocalizationStrings;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Localization.Controllers
{
  [ApiController, Route("localization")]
  public class LocalizationStringController : ControllerBase
  {
    private readonly IMapper _mapper;

    public LocalizationStringController(IMapper mapper)
    {
      _mapper = mapper;
    }

    [HttpPost]
    public ActionResult<LocalizationStringItemView> Add(LocalizationStringItemView view)
    {
      var dto = _mapper.Map<LocalizationRowDto>(view);
      view = _mapper.Map<LocalizationStringItemView>(dto);
      return view;
    }

    [HttpPut("{id}")]
    public ActionResult<LocalizationStringItemView> Update(long id, LocalizationStringItemView view)
    {
      var dto = _mapper.Map<LocalizationRowDto>(view);
      view = _mapper.Map<LocalizationStringItemView>(dto);
      return view;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      return Ok();
    }

    [HttpGet]
    public ActionResult<LocalizationStringListView> GetAll()
    {
      var item = new LocalizationRowDto(0,
        "group",
        "key",
        new List<LocalizationPairDto>
        {
          new LocalizationPairDto("ru", "sdfgsdfg"),
          new LocalizationPairDto("ua", "@222"),
          new LocalizationPairDto("en", "!11"),
        });

      var list = Enumerable
        .Repeat(item, 100)
        .Select(_mapper.Map<LocalizationStringItemView>);

      return new LocalizationStringListView {LocalizationStrings = list};
    }

    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(new[] {"ru", "ua", "en"});
    }
  }
}