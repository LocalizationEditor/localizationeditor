using Newtonsoft.Json;

namespace Localization.Area.Auth.Models
{
  public class JwtTokenView
  {
    [JsonProperty("token")]
    public string Token { get; set; }
  }
}