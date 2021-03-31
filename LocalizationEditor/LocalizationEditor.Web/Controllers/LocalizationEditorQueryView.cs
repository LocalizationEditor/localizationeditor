using Newtonsoft.Json;

namespace LocalizationEditor.Web.Controllers
{
  public class LocalizationEditorQueryView
  {
    [JsonProperty("groupKey")]
    public string GroupKey { get; set; }
    [JsonProperty("localizationStringKey")]
    public string LocalizationStringKey { get; set; }
  }
}