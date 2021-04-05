using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.LocalizationStrings
{
  public class LocalizationEditorQueryView
  {
    [JsonProperty("groupKey")]
    public string GroupKey { get; set; }
    [JsonProperty("localizationStringKey")]
    public string LocalizationStringKey { get; set; }
  }
}