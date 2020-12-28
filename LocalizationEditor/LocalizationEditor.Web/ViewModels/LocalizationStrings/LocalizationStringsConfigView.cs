using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationStringsConfigView
  {
    [JsonProperty("locales")]
    public IEnumerable<string> Locales { get; set; }
  }
}