using Newtonsoft.Json;

namespace Localization.ViewModels.LocalizationStrings
{
  public class LocalizationPairView
  {
    [JsonProperty("locale")]
    public string Locale { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
  }
}