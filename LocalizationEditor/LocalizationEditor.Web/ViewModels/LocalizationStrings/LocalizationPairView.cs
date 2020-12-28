using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationPairView
  {
    [JsonProperty("locale")]
    public string Locale { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
  }
}