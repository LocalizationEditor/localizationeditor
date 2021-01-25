using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationStringGroupView
  {
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}