using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public IActionResult Add(LocalizationStringItemView view)
    {
      return null;
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, LocalizationStringItemView view)
    {
      return null;
    }

    [HttpGet("{id}")]
    public ActionResult<LocalizationStringItemView> GetById(long id)
    {
      return null;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      return Ok();
    }

    [HttpGet]
    public ActionResult<LocalizationStringList> GetAll()
    {
      var item = new LocalizationStringItemView {
        Id = 0,
        Key = "key",
        Group = "group" ,
        Localizations = new []
        {
          new LocalizationPair{Locale = "ru", Value = "sdfgsdfg"},
          new LocalizationPair{Locale = "ua", Value = "@222"},
          new LocalizationPair{Locale = "en", Value = "!11"},
        }};
      var list = Enumerable.Repeat(item, 100);
      return new LocalizationStringList {LocalizationStrings = list};
    }

    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return _mapper.Map<LocalizationStringsConfigView>(new[] {"ru", "ua", "en"});
    }
  }


  public class LocalizationStringItemView
  {
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("group")]
    public string Group { get; set; }
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("localizations")]
    public IEnumerable<LocalizationPair> Localizations { get; set; }
  }

  public class LocalizationPair
  {
    [JsonProperty("locale")]
    public string Locale { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }

  }

  public class LocalizationStringList
  {
    [JsonProperty("localizationStrings")]
    public IEnumerable<LocalizationStringItemView> LocalizationStrings { get; set; }
  }

  public class LocalizationStringsConfigView
  {
    [JsonProperty("locales")]
    public IEnumerable<string> Locales { get; set; }
  }

  public class LocalizationStringsConfigViewMapperProfile : Profile
  {
    public LocalizationStringsConfigViewMapperProfile()
    {
      CreateMap<IEnumerable<string>, LocalizationStringsConfigView>()
        .ForMember(i=>i.Locales,
          act=>act.MapFrom(i=>i));
    }
  }

  public class LocalizationItemViewMapperProfile : Profile
  {
    public LocalizationItemViewMapperProfile()
    {
      CreateMap<IEnumerable<string>, LocalizationStringsConfigView>()
        .ForMember(i=>i.Locales,
          act=>act.MapFrom(i=>i));
    }
  }
}