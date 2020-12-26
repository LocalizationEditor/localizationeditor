using Newtonsoft.Json;
using System.Collections.Generic;

namespace Localization.ViewModels.LocalizationStrings
{
  public class LocalizationStringsConfigView
  {
    [JsonProperty("locales")]
    public IEnumerable<string> Locales { get; set; }
  }
}