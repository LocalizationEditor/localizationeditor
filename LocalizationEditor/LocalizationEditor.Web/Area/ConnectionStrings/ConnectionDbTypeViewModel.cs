using Newtonsoft.Json;

namespace LocalizationEditor.Web.Area.ConnectionStrings
{
  public class ConnectionDbTypeViewModel
  {
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}