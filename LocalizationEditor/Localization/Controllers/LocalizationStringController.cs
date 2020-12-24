using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Localization.Controllers
{
  [ApiController, Route("localization")]
  public class LocalizationStringController : ControllerBase
  {
    public IActionResult Add(LocalizationStringItemView view)
    {
      return null;
    }

    public IActionResult Update(long id, LocalizationStringItemView view)
    {
      return null;
    }

    public ActionResult<LocalizationStringItemView> GetById(long id)
    {
      return null;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      return Ok();
    }

    public ActionResult<LocalizationStringList> GetAll()
    {
      var x = Enumerable.Empty<LocalizationStringItemView>();
      return new LocalizationStringList {LocalizationStrings = x};
    }

    [HttpGet("config")]
    public ActionResult<LocalizationStringsConfigView> GetConfig()
    {
      return new LocalizationStringsConfigView {Locales = new[] {"ru", "ua", "en"}};
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
}