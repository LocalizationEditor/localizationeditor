using LocalizationEditor.Base.Utils.Json;
using Newtonsoft.Json;

namespace Localization.Area.Auth.Models
{
  public class LoginModelView
  {
    [JsonProperty("email")]
    [SensitiveProperty]
    public string Email { get; set; }

    [JsonProperty("password")]
    [SensitiveProperty]
    public string Password { get; set; }
  }
}