using Newtonsoft.Json;

namespace LocalizationEditor.Web.Area.ConnectionStrings
{
  public class ConnectionConfigViewModel
  {
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}