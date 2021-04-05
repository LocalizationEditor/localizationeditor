using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationStringsEditorConfig : LocalizationStringsConfigView
  {
    [JsonProperty("groups")]
    public IEnumerable<string> Groups { get; set; }
  }
}