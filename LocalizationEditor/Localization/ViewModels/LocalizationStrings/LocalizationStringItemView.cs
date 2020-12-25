using Newtonsoft.Json;
using System.Collections.Generic;

namespace Localization.ViewModels.LocalizationStrings
{
  public class LocalizationStringItemView
  {
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("group")]
    public string Group { get; set; }
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("localizations")]
    public IEnumerable<LocalizationPairView> Localizations { get; set; }
  }
}