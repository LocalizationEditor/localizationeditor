using Newtonsoft.Json;
using System.Collections.Generic;

namespace Localization.ViewModels.LocalizationStrings
{
  public class LocalizationStringListView
  {
    [JsonProperty("localizationStrings")]
    public IEnumerable<LocalizationStringItemView> LocalizationStrings { get; set; }
  }
}