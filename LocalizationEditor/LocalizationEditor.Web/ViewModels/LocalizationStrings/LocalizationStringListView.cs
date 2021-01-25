using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationStringListView
  {
    [JsonProperty("localizationStrings")]
    public IEnumerable<LocalizationStringItemView> LocalizationStrings { get; set; }
    [JsonProperty("count")]
    public int Count { get; set; }
  }
}