using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.Base
{
  public class IdView
  {
    [JsonProperty("id")]
    public long Id { get; set; }
  }
}