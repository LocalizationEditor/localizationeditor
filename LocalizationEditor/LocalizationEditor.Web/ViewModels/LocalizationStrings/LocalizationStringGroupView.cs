using LocalizationEditor.Web.ViewModels.Base;
using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationStringGroupView : IdView
  {
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}