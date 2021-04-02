using Newtonsoft.Json;

namespace LocalizationEditor.Web.Controllers.ConnectionStrings
{
  public class IdNamePairView
  {
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}