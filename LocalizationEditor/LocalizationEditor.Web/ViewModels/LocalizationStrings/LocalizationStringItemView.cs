using LocalizationEditor.Web.ViewModels.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationStringItemView : IdView
  {
    [JsonProperty("group")]
    public string Group { get; set; }
    [JsonProperty("key")]
    public string Key { get; set; }
    [JsonProperty("localizations")]
    public IEnumerable<LocalizationPairView> Localizations { get; set; }
  }
}